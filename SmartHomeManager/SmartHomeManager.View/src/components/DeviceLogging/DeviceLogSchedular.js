import React, { useState, useEffect } from "react";
import axios from "axios";
import Datetime from 'react-datetime';
import { format } from 'date-fns'

import { Button, Box, Heading, Text, Select } from "@chakra-ui/react";
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from "chart.js";


ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

function DeviceLogSchedular() {
    const [deviceID, setDevice1ID] = useState("");
    const [deviceState, setDeviceState] = useState(false);
    const [allDeviceID, setAllDeviceID] = useState([]);
    const [logInterval, setLogInterval] = useState("");

    const onDeviceListChange = (event) => {
        const value = event.target.value;
        setDevice1ID(value);
    };

    const onTimeIntervalChange = (event) => {
        const value = event.target.value;
        setLogInterval(value);
    };

    useEffect(() => {
        const fetchDeviceID = async () => {
            try {
                const res = await axios.get(`https://localhost:7140/api/DeviceLog/{22222222-2222-2222-2222-222222222222}`)
                const data = await res.data
                setAllDeviceID(data)
            } catch (error) {
                console.error(error)
            }
        }
        fetchDeviceID()
    }, [])

    const putLogOnStateOff = async (id, date) => {
        try {
            for (let i = 0; i < 7; i++) {
                //create the post request
                const res = await axios.put(`https://localhost:7140/api/DeviceLog/stateOff/${id}/${date}`)

                // result from backend
                let result = await res.data
                console.log(result)
                

            }

        } catch (err) {
            console.error(err)
        }
    }

    // displaying 
    return (
        <Box>
            <Heading alignContent="center">Device Log Schedular</Heading>
            <Text>This page displays the logs of the device that is running</Text>

            <Box paddingTop="5">

                <text>Please select device to switch off{'\n'}</text>
                <Select
                    onChange={onDeviceListChange}
                    defaultValue={""}
                    variant='filled'
                >
                    <option disabled={true} value="">Please select device</option>
                    {
                        allDeviceID.map((list, index) =>
                            <option key={index} value={list}> Device {index}</option>)
                    }
                </Select>  

                <Box paddingTop="5">
                    <Button
                        padding="5"
                        colorScheme="blue"
                        onClick={() => {
                            setDeviceState(false)
                            putLogOnStateOff(deviceID, "2023-02-19 00:00:00")
                        }}
                    >
                        Switch off
                    </Button>
                </Box>

                <text>Please select whether to log hourly or daily{'\n'}</text>
                <Select
                    onChange={onTimeIntervalChange}
                    defaultValue={""}
                    variant='filled'
                >
                    <option disabled={true} value="">Please select interval</option>
                    <option value="hourly">Hourly</option>
                    <option value="daily">Daily</option>
                </Select>
            </Box>
        </Box>
    );
};
export default DeviceLogSchedular;