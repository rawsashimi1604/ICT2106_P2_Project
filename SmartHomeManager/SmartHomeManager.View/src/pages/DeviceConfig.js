import React, { useState } from "react";
import { Select } from '@chakra-ui/react';
import {
    Slider,
    SliderTrack,
    SliderFilledTrack,
    SliderThumb,
    SliderMark,
    Box
} from '@chakra-ui/react'

export default function DeviceConfig() {

    const [DropDownValue, setDropDownValue] = useState(null);
    const [sliderValue, setSliderValue] = useState(50)


    const labelStyles = {
        mt: '2',
        ml: '-2.5',
        fontSize: 'sm',
    }

    const dropDownHandler = (e) => {
        let value = e.target.value;
        setDropDownValue(value);
        if (value === "option1") {
            setSliderValue(50)
        } else if (value === "option2") {
            setSliderValue(30)
        }
    };

    return (
        <div> <strong>Set Threshold Limit for devices</strong>
            <br></br>
            <br></br>
            <Select placeholder='Select Device'
                width={"200px"}
                onChange={(e) => dropDownHandler(e)}>
                <option value='option1'>Device 1</option>
                <option value='option2'>Device 2</option>
            </Select>
            {DropDownValue ? (
                <div style={{ width: "600px" }}>
                    <br></br>
                    <Box pt={6} pb={2}>
                        <Slider aria-label='slider-ex-6' onChange={(val) => setSliderValue(val)}>
                            <SliderMark value={25} {...labelStyles}>
                                25
                            </SliderMark>
                            <SliderMark value={50} {...labelStyles}>
                                50
                            </SliderMark>
                            <SliderMark value={75} {...labelStyles}>
                                75
                            </SliderMark>
                            <SliderMark value={100} {...labelStyles}>
                                100
                            </SliderMark>
                            <SliderMark
                                value={sliderValue}
                                textAlign='center'
                                bg='blue.500'
                                color='white'
                                mt='-10'
                                ml='-5'
                                w='12'
                            >
                                {sliderValue}
                            </SliderMark>
                            <SliderTrack>
                                <SliderFilledTrack />
                            </SliderTrack>
                            <SliderThumb />
                        </Slider>
                    </Box>
                    <div><br></br>Limit is: <strong>{sliderValue}</strong></div>
                </div>
            ) : (
                <div>
                    <strong>Please select any value from dropdown</strong>
                </div>
            )}
        </div>
        );
}

