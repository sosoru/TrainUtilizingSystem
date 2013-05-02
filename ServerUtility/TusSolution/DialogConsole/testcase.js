var testswitchdata = [
{
    "CurrentState": {
        "ChangingTime": 0,
        "DeadTime": 100,
        "ID": "(1, 2, 1)",
        "ModuleType": "AvrSwitch",
        "Position": "Straight"
    },
    "DeviceID": "(1, 2, 1)",
    "ModuleType": "AvrSwitch"
},
{
    "CurrentState": {
        "ChangingTime": 0,
        "DeadTime": 100,
        "ID": "(1, 2, 3)",
        "ModuleType": "AvrSwitch",
        "Position": "Curve"
    },
    "DeviceID": "(1, 2, 3)",
    "ModuleType": "AvrSwitch"
},
{
    "CurrentState": {
        "ChangingTime": 0,
        "DeadTime": 100,
        "ID": "(1, 2, 2)",
        "ModuleType": "AvrSwitch",
        "Position": "Any"
    },
    "DeviceID": "(1, 2, 2)",
    "ModuleType": "AvrSwitch"
}];

var testblockdata = [
    {
        "Devices": [],
        "Name": "AT1"
    },
    {
        "Devices": [
            {
                "CurrentState": {
                    "ChangingTime": 0,
                    "DeadTime": 100,
                    "ID": "(1, 2, 1)",
                    "ModuleType": "AvrSwitch",
                    "Position": "Straight"
                },
                "DeviceID": "(1, 2, 1)",
                "ModuleType": "AvrSwitch",
                "__type": "Switch:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "AT2"
    },
    {
        "Devices": [
            {
                "CurrentState": {
                    "ID": "(1, 3, 1)",
                    "ModuleType": "AvrSensor",
                    "Threshold": 0,
                    "Voltage": 0,
                    "VoltageOff": 0,
                    "VoltageOn": 0
                },
                "DeviceID": "(1, 3, 1)",
                "ModuleType": "AvrSensor",
                "__type": "UsartSensor:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "AT3"
    },
    {
        "Devices": [
            {
                "CurrentMemory": "Unknown",
                "CurrentState": {
                    "ControlMode": "Unknown",
                    "Current": 0,
                    "DestinationID": "(0, 0, 0)",
                    "DestinationMemory": "Unknown",
                    "Direction": "Standby",
                    "Duty": 0,
                    "ID": "(1, 1, 1)",
                    "MemoryWhenEntered": "Unknown",
                    "ModuleType": "AvrMotor",
                    "ThresholdCurrent": 0
                },
                "DeviceID": "(1, 1, 1)",
                "DeviceKernel": {
                    "CurrentState": {
                        "Command": 0,
                        "ID": "(1, 1, 1)",
                        "ModuleType": "AvrKernel"
                    },
                    "DeviceID": "(1, 1, 1)",
                    "ModuleType": "AvrKernel"
                },
                "IsDetected": false,
                "ModuleType": "AvrMotor",
                "States": [],
                "__type": "Motor:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "AT4"
    },
    {
        "Devices": [],
        "Name": "BT1"
    },
    {
        "Devices": [
            {
                "CurrentState": {
                    "ChangingTime": 0,
                    "DeadTime": 100,
                    "ID": "(1, 2, 3)",
                    "ModuleType": "AvrSwitch",
                    "Position": "Straight"
                },
                "DeviceID": "(1, 2, 3)",
                "ModuleType": "AvrSwitch",
                "__type": "Switch:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "BT2"
    },
    {
        "Devices": [
            {
                "CurrentState": {
                    "ID": "(1, 3, 2)",
                    "ModuleType": "AvrSensor",
                    "Threshold": 0,
                    "Voltage": 0,
                    "VoltageOff": 0,
                    "VoltageOn": 0
                },
                "DeviceID": "(1, 3, 2)",
                "ModuleType": "AvrSensor",
                "__type": "UsartSensor:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "BT3"
    },
    {
        "Devices": [
            {
                "CurrentMemory": "Unknown",
                "CurrentState": {
                    "ControlMode": "Unknown",
                    "Current": 0,
                    "DestinationID": "(0, 0, 0)",
                    "DestinationMemory": "Unknown",
                    "Direction": "Standby",
                    "Duty": 0,
                    "ID": "(1, 1, 2)",
                    "MemoryWhenEntered": "Unknown",
                    "ModuleType": "AvrMotor",
                    "ThresholdCurrent": 0
                },
                "DeviceID": "(1, 1, 2)",
                "DeviceKernel": {
                    "CurrentState": {
                        "Command": 0,
                        "ID": "(1, 1, 2)",
                        "ModuleType": "AvrKernel"
                    },
                    "DeviceID": "(1, 1, 2)",
                    "ModuleType": "AvrKernel"
                },
                "IsDetected": false,
                "ModuleType": "AvrMotor",
                "States": [],
                "__type": "Motor:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "BT4"
    },
    {
        "Devices": [
            {
                "CurrentState": {
                    "ID": "(1, 3, 3)",
                    "ModuleType": "AvrSensor",
                    "Threshold": 0,
                    "Voltage": 0,
                    "VoltageOff": 0,
                    "VoltageOn": 0
                },
                "DeviceID": "(1, 3, 3)",
                "ModuleType": "AvrSensor",
                "__type": "UsartSensor:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "CT1"
    },
    {
        "Devices": [
            {
                "CurrentState": {
                    "ChangingTime": 0,
                    "DeadTime": 100,
                    "ID": "(1, 2, 2)",
                    "ModuleType": "AvrSwitch",
                    "Position": "Straight"
                },
                "DeviceID": "(1, 2, 2)",
                "ModuleType": "AvrSwitch",
                "__type": "Switch:#Tus.Communication.Device.AvrComposed"
            }
        ],
        "Name": "CT2"
    }
];