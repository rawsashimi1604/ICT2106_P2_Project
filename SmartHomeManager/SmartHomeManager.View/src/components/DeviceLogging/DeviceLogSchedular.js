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
import WeeklyChart from "./WeeklyChart";
import DailyChart from "./DailyChart";
import { data } from "../Analytics/EfficiencyBar";


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
    const [allDevices, setAllDevices] = useState([]);
    const [room1ComparisionSelect, setRoom1ComparisionSelect] = useState(null);
    const [room2ComparisionSelect, setRoom2ComparisionSelect] = useState(null);
    const [dataType, setDataType] = useState("")
    const [chartVisible, setChartVisible] = useState(false);
    const [isChart1Visible, setIsChart1Visible] = useState(true);
    const [isChart2Visible, setIsChart2Visible] = useState(false);




    const onRoomChange = (event) => {
        const value = event.target.value;
        setRoomSelect(value);
    };

    const onRoom1ComparisionChange = (event) => {
        const value = event.target.value;
        setRoom1ComparisionSelect(value);
    };

    const onRoom2ComparisionChange = (event) => {
        const value = event.target.value;
        setRoom2ComparisionSelect(value);
    };

    const onDataTypeChange = (event) => {
        const value = event.target.value;
        setDataType(value);
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

    const generateWeeklyCharts =  (room1, room2, dataType) => {
        try {
            console.log(room1)
           return <WeeklyChart room1={room1} room2={room2} dataType={dataType}  />
        } catch (error) {
            console.log(error)
        }
    }

    const generateDailyCharts = (room1, room2, dataType) => {
        try {
            console.log(room1)
            return <DailyChart room1={room1} room2={room2} dataType={dataType} />
        } catch (error) {
            console.log(error)
        }
    }

    const handleWeeklyButtonClick = () => {
        setIsChart1Visible(true);
        setIsChart2Visible(false);
    }
    const handleDailyButtonClick = () => {
        setIsChart1Visible(false);
        setIsChart2Visible(true);
    }


    // displaying 
    return (
        <Box>
            <Heading alignContent="center">Device Log Schedular</Heading>
            <Text>This page displays the logs of the device that is running</Text>
            <Text> Debug space </Text>
            {roomList.map((room, index) => 
                <Text key={ index}> {room.roomId}</Text>)
            }  

            <Box>
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

            <Box>
                <Heading alignContent="center">Room Comparision</Heading>
                <Text>Compares device energy usage or activity level between rooms</Text>
                <Text>{room1ComparisionSelect}</Text>
            </Box>

            <Box>
                <Text>Select the first room to compare</Text>
                <Select
                    onChange={onRoom1ComparisionChange}
                    defaultValue={""}
                    variant='filled'
                >
                    <option disabled={true} value=""> Select 1st room </option>
                    {
                        roomList.map((room, index) =>
                            <option key={index} value={room.roomId}> {room.name}</option>
                        )
                    }

                </Select>

                <Box>
                    <Text>Select the second room to compare</Text>
                    <Select
                        onChange={onRoom2ComparisionChange}
                        defaultValue={""}
                        variant='filled'
                    >
                        <option disabled={true} value=""> Select 2nd room </option>
                        {
                            roomList.map((room, index) =>
                                <option key={index} value={room.roomId}> {room.name}</option>
                            )
                        }
                    </Select>
                </Box>

                <Box>
                    <Text>Choose to view energy or activity</Text>
                    <Select
                        onChange={onDataTypeChange}
                        defaultValue={""}
                        variant="filled"
                    >
                        <option disabled={true} value="">
                            Select data type to compare
                        </option>
                        <option value="energyUsage">Energy Usage</option>
                        <option value="activityLevel">Activity Level</option>
                    </Select>
                </Box>

                <Box>
                    <Button onClick={handleWeeklyButtonClick}>
                        Generate Weekly Charts
                        {isChart1Visible && generateWeeklyCharts(room1ComparisionSelect, room2ComparisionSelect, dataType)}
                    </Button>
                    <Button onClick={handleDailyButtonClick}>
                        Generate daily Charts
                        {isChart2Visible && generateDailyCharts(room1ComparisionSelect, room2ComparisionSelect, dataType)}
                    </Button>
                </Box>

            </Box>
        </Box>
    );
};
export default DeviceLogSchedular;