import React, { useState, useEffect } from "react";
import axios from "axios";
import Datetime from 'react-datetime';
import { format } from 'date-fns'
import DeviceSchedularFilter from "components/DeviceLogging/DeviceSchedularFilter"

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
import Rooms from "../../pages/Rooms";


ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

function DeviceLogSchedular() {
    const [roomList, setRoomList] = useState([])
    const [deviceID, setDevice1ID] = useState("");
    const [roomSelect, setRoomSelect] = useState("")
    const [deviceState, setDeviceState] = useState(false);
    const [allDeviceID, setAllDeviceID] = useState([]);
    const [logInterval, setLogInterval] = useState("");
    const [allDevices, setAllDevices] = useState([])



    const onRoomChange = (event) => {
        const value = event.target.value;
        setRoomSelect(value);
    };

    useEffect(() => {
        const fetchRoom = async () => {
            try {
                const res = await axios.get(`https://localhost:7140/api/DeviceLog/getRooms`)
                const data = await res.data
                setRoomList(data)
            } catch (error) {
                console.error(error)
            }
        }
        fetchRoom() 
    }, [])

    

    // displaying 
    return (
        <Box>
            <Heading alignContent="center">Device Log Schedular</Heading>
            <Text>This page displays the logs of the device that is running</Text>
            <Text> Debug space </Text>
            {roomList.map((room, index) => 
                <Text key={ index}> {room.roomId}</Text>)
            }  

            <Box paddingTop="5">
                <Text>Select room</Text>
                <Select
                    onChange={onRoomChange}
                    defaultValue={""}
                    variant='filled'
                >
                    <option disabled={true} value=""> Select a room </option>
                    {
                        roomList.map((room, index) =>
                            <option key={index} value={room.roomId}> {room.name }</option>
                            )
                    }
                    
                </Select>

                <DeviceSchedularFilter room ={roomSelect} />
            
            </Box>
        </Box>
    );
};
export default DeviceLogSchedular;