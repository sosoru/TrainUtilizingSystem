/*
 * sc2004.hpp
 *
 * Created: 2012/05/16 8:30:38
 *  Author: Administrator
 */ 


#ifndef SC2004_H_
#define SC2004_H_

#include "../avr_base.hpp"
#include <util/delay.h>

namespace EthernetBridge
{
	namespace Lcd
	{
		enum IncrementDirection
		{
			DirectionLeft = 0,
			DirectionRight = 1,
		};
		
		enum DisplayShift
		{
			DisplayShiftEnable = 1,
			DisplayShiftDisable = 0
		};
		
		enum DisplayPower
		{
			DisplayOn = 1,
			DisplayOff = 0,
		};
		
		enum CursorEnable
		{
			CursorShown = 1,
			CursorHidden = 0,
		};
		
		enum CursorBlink
		{
			CursorBlinking = 1,
			CursorNotBlinking = 0,
		};
		
		enum ShiftOf
		{
			ShiftDisplay = 0,
			ShiftCursor = 1,
		};
		
		enum ShiftDirection
		{
			ShiftToLeft = 0,
			ShiftToRight = 1,
		};
		
		template<
				class t_data_port,	// Port<> class
				class t_rs_outpin,
				class t_rw_outpin,
				class t_enable_outpin
				>
		class Lcd_sc2004
		{
			private :
			
			static inline void OutData(uint8_t data)	 { t_data_port::SetAsOutput(0xff); t_data_port::Output::Set(data); }
			static inline void InData()				 { t_data_port::SetAsInput(0x00); }
			static inline uint8_t GetData()				 { return t_data_port::Input::Get(); }
			
			static inline void SetReadFlag() { t_rw_outpin::Set(); }
			static inline void SetWriteFlag() { t_rw_outpin::Clear(); }
			
			static inline void SetSystemCommandFlag() { t_rs_outpin::Clear(); }
			static inline void SetDataCommandFlag() { t_rs_outpin::Set();}
				
			static inline void SetEnable() {t_enable_outpin::Set();}
			static inline void ClearEnable() {t_enable_outpin::Clear();}
						
			static inline void PulseEnable()
			{
				ClearEnable();
				
				SetEnable();
				_delay_us(1);
				
				ClearEnable();
			}
			
			static void SetFunction(uint8_t func)
			{
				SetSystemCommandFlag();
				SetWriteFlag();
				
				OutData( (1<<5)
						| (func<<2));
				
				PulseEnable();
			}
						
			template<bool t_busy_only>
			static inline bool IsBusyAndReadAddressInner(uint8_t &addr)
			{
				SetSystemCommandFlag();
				SetReadFlag();
				InData();
				
				PulseEnable();
				
				uint8_t received = GetData();
				
				if(!t_busy_only)
				{
					addr = received & 0b01111111;
				}					
				
				return (received >> 7) &1;
					
			}
			
			protected:
			
			static const uint8_t WIDTH = 20;
			static const uint8_t HEIGHT = 4;
			
			static void InitPort()
			{
				t_rs_outpin::InitOutput();
				t_rw_outpin::InitOutput();
				t_enable_outpin::InitOutput();
				
				t_rs_outpin::Clear();
				t_rw_outpin::Clear();	
				t_enable_outpin::Clear();
				
				OutData(0);
			}
						
			static void InitLcd()
			{
				InitPort();
				// busy flag cannot be read before initialization completed.
	
				_delay_ms(15);
				SetFunction(0b100);
				_delay_ms(5);
				SetFunction(0b100);
				_delay_ms(5);
				SetFunction(0b100);
				_delay_ms(5);

				SetFunction(0b100);
				_delay_us(40);
	
				//8-bit, dual line, 5x8 dots
				SetFunction(0b110);
				_delay_us(40);
		
				DisplayMode(DisplayOff, CursorHidden, CursorNotBlinking);
				_delay_us(40);
				
				DisplayMode(DisplayOn, CursorHidden, CursorNotBlinking);
				_delay_us(40);
				
				ClearDisplay();
				_delay_ms(2);

				EntryMode(DirectionRight, DisplayShiftDisable);
				_delay_us(40);

			}


			public :
			
			static void ClearDisplay()
			{
				SetSystemCommandFlag();
				SetWriteFlag();
					
				OutData(0b00000001);
				
				PulseEnable();
			}
			
			static void ReturnHome()
			{
				SetSystemCommandFlag();
				SetWriteFlag();
				
				OutData(0b00000010);
				
				PulseEnable();
			}
			
			static void EntryMode(IncrementDirection increment_dir, DisplayShift display_shift)
			{
				SetSystemCommandFlag();
				SetWriteFlag();
				
				OutData( (1<<2)
						| (increment_dir<<1)
						| (display_shift<<0));
				
				PulseEnable();
			}
			
			static void DisplayMode(DisplayPower disp_pwr, CursorEnable cursor_enable, CursorBlink cursor_blink)
			{
				SetSystemCommandFlag();
				SetWriteFlag();
				
				OutData( (1<<3)
						| (disp_pwr<<2)
						| (cursor_enable<<1)
						| (cursor_blink<<0));
						
				PulseEnable();
			}
			
			static void CursorDisplayShift(ShiftOf shift_of, ShiftDirection shift_dir)
			{
				SetSystemCommandFlag();
				SetWriteFlag();
				
				OutData( (1<<4)
						| (shift_of<<3)
						| (shift_dir<<2));
						
				PulseEnable();
			}
			
			static void SetAddressOfCGRAM(uint8_t addr)
			{
				SetSystemCommandFlag();
				SetWriteFlag();
				
				OutData((1<<6)
						| (addr & 0b00111111));
				
				PulseEnable();
			}
			
			static void SetAddressOfDDRAM(uint8_t addr)
			{
				SetSystemCommandFlag();
				SetWriteFlag();
				
				OutData((1<<7)
						| (addr & 0b01111111));
				
				PulseEnable();
			}
			
			static void SetPosition(uint8_t pos)
			{
				uint8_t addr;
				
				if(pos < 20)
					addr = pos - WIDTH * 0;
				else if (pos < 40)
					addr = pos - WIDTH * 1 + 0x40;
				else if (pos < 60)
					addr = pos - WIDTH * 2 + 0x14;
				else
					addr = pos - WIDTH * 3 + 0x54;
				
				SetAddressOfDDRAM(addr);
			}
			
			static bool IsBusy()
			{
				uint8_t tmp;
				return IsBusyAndReadAddressInner<true>(tmp);
			}		
			
			static bool IsBusyAndReadAddress(uint8_t &addr)
			{
				return IsBusyAndReadAddressInner<false>(addr);
			}
			
			static void WriteData(uint8_t data)
			{
				SetDataCommandFlag();
				SetWriteFlag();
				
				OutData(data);
				
				PulseEnable();
			}
			
			static uint8_t ReadData()
			{
				SetDataCommandFlag();
				SetReadFlag();
				
				InData();
				
				PulseEnable();
				
				return GetData();
			}
		};
	}
}


#endif /* SC2004_H_ */