
#ifndef INC_MOTOR_ADC
#define INC_MOTOR_ADC

void adc_init();
void adc_change(uint8_t channel);
void adc_start();
void adc_wait();
uint8_t adc_result();

#endif