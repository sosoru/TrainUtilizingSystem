-- trivial protocol example
-- プロトコルの定義
tus_proto = Proto("tus","TUS","TUS Protocol")
-- パース用の関数定義
function tus_proto.dissector(buffer,pinfo,tree)

    local src_eth = buffer(0,1):uint()
    local src_int = buffer(2,1):uint()
    local src_mdl = buffer(3,1):uint()

    local dst_eth = buffer(4,1):uint()
    local dst_int = buffer(6,1):uint()
    local dst_mdl = buffer(7,1):uint()

    pinfo.cols.protocol = "tus"
    pinfo.cols.info = string.format("tus : (%d, %d, %d) -> (%d, %d, %d)",src_eth, src_mdl, src_int, dst_eth, dst_mdl, dst_int)

	dissector(TvbRange.tvb(buffer(12, buffer:len()-12)), pinfo, tree)
	
end

function dissector(buffer, pinfo, tree)
	local bufind = 0
	local PACKET_DATA_LEN = 28
	
	local top = tree:add(
		buffer(bufind,buffer:len()),
		"tus packet : " .. tostring(buffer:len()) .. " byte(s)")
	
	while bufind < PACKET_DATA_LEN and buffer(bufind,1):uint() ~= 0x00 do
		local len = buffer(bufind,1):uint()
		local internalid = buffer(bufind+1,1):uint()
		local mtype = buffer(bufind+2,1):uint()
		local subtvb = TvbRange.tvb(buffer(bufind+3,len-3))
		
		--dispatch
		if mtype == 0x11 then 
			AvrKernelDissector(subtvb, internalid, top)
		elseif mtype == 0x12 then --AvrMotor
			AvrMotorDissector(subtvb, internalid, top)
		elseif mtype == 0x13 then --AvrSwitch
		elseif mtype == 0x14 then --AvrSensor
		elseif mtype == 0x15 then --AvrUartSetting
		end
		
		bufind = bufind + len
	end
end

function AvrKernelDissector(buffer, id, tree)
	local command = buffer(0,1):uint()
	local commandstr = ""
	
	if command == 0x01 then
		commandstr = "Inquiry"
	elseif command == 0x02 then
		commandstr = "Memory"
	else
		commandstr = "undefined"
	end
	
	local sub = tree:add(
		buffer(0,buffer:len()),
		string.format("(%d)AvrKernel: %s", id, commandstr))
	sub:add(
		buffer(0,1),
		string.format("Command: %s", commandstr))
	sub:add(
		buffer(1,1),
		string.format("Content1: %d", buffer(1,1):uint()))
	sub:add(
		buffer(2,1),
		string.format("Content2: %d", buffer(2,1):uint()))
	sub:add(
		buffer(3,1),
		string.format("Content3: %d", buffer(3,1):uint()))
	sub:add(
		buffer(4,1),
		string.format("Content4: %d", buffer(4,1):uint()))
end

function AvrMotorDissector(buffer, id, tree)
	local controlmode ={
		"unknown",
		"DutySpecified",
		"CurrentFeedBack",
		"WaitingPulse",
	}
	local direction={
		"Standby",
		"Positive",
		"Negative",
	}
	
	local sub = tree:add(
		buffer(0,buffer:len()),
		string.format("(%d)AvrMotor: mem: %d, mode: %s",
						id,
						buffer(1,1):uint(),
						controlmode[1+buffer(0,1):uint()]
						))
		
	sub:add(
		buffer(0,1),
		string.format("ControlMode: %s", controlmode[1+buffer(0,1):uint()]))
	sub:add(
		buffer(1,1),
		string.format("CurrentMemory: %d", buffer(1,1):uint()))
	sub:add(
		buffer(2,1),
		string.format("Direction: %s", direction[1+buffer(2,1):uint()]))
	sub:add(
		buffer(3,1),
		string.format("Duty: %d", buffer(3,1):uint()))
	sub:add(
		buffer(4,1),
		string.format("Current: %d", buffer(4,1):uint()))
	sub:add(
		buffer(5,4),
		string.format("DestinationID: (%d, %d, %d)", buffer(5,1):uint(), buffer(7,1):uint(), buffer(8,1):uint()))
	sub:add(
		buffer(9,1),
		string.format("TransitMemory: %d", buffer(9,1):uint()))
end

-- udp.portテーブルのロード
udp_table = DissectorTable.get("udp.port")
-- ポート8000番とプロトコルの紐付けをする
udp_table:add(8000,tus_proto)
udp_table:add(8001,tus_proto)
