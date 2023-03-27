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
    const [roomSelect, setRoomSelect] = useState("")
    const [room1Id, setRoom1Id] = useState(null);
    const [room2Id, setRoom2Id] = useState(null);
    const [room1Name, setRoom1Name] = useState(null);
    const [room2Name, setRoom2Name] = useState("");
    const [dataType, setDataType] = useState("")
    const [isChart1Visible, setIsChart1Visible] = useState(false);
    const [isChart2Visible, setIsChart2Visible] = useState(false);




    const onRoomChange = (event) => {
        const value = event.target.value;
        setRoomSelect(value);
    };

    const onRoom1ComparisionChange = (roomId, roomName) => {
        // handle room selection here, using roomId and roomName
        setRoom1Id(roomId);
        setRoom1Name(roomName)

    }

 
    const onRoom2ComparisionChange = (roomId, roomName) => {
        // handle room selection here, using roomId and roomName
        setRoom2Id(roomId);
        setRoom2Name(roomName)
       
    }


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

    const generateWeeklyCharts =  (room1, room2, room1Name, room2Name, dataType) => {
        try {
            return <WeeklyChart room1={room1} room2={room2} room1Name={room1Name} room2Name={ room2Name} dataType={dataType}  />
        } catch (error) {
            console.log(error)
        }
    }

    const generateDailyCharts = (room1, room2, room1Name, room2Name, dataType) => {
        try {
            return <DailyChart room1={room1} room2={room2} room1Name={room1Name} room2Name={room2Name} dataType={dataType} />
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
                <Heading alignContent="center">Room Comparison</Heading>
                <Text>Compares device energy usage or activity level between rooms</Text>
            </Box>

            <Box>
                <Text>Select the first room to compare</Text>
                <Select
                    onChange={(event) => onRoom1ComparisionChange(event.target.value, event.target.options[event.target.selectedIndex].text)}
                    defaultValue=""
                    variant="filled"
                >
                    <option disabled value="">
                        Select 1st room
                    </option>
                    {roomList.map((room) => (
                        <option key={room.roomId} value={room.roomId}>
                            {room.name}
                        </option>
                    ))}
                </Select>

                <Box>
                    <Text>Select the second room to compare</Text>
                    <Select
                        onChange={(event) => onRoom2ComparisionChange(event.target.value, event.target.options[event.target.selectedIndex].text)}
                        defaultValue=""
                        variant="filled"
                    >
                        <option disabled value="">
                            Select 2nd room
                        </option>
                        {roomList.map((room) => (
                            <option key={room.roomId} value={room.roomId}>
                                {room.name}
                            </option>
                        ))}
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

                <Box display="flex" flexDirection="row" marginTop="5">
                    <Box marginRight={4}>
                        <Button onClick={handleWeeklyButtonClick}>
                            Generate Weekly Charts
                        </Button>
                    </Box>

                    <Box>
                        <Button onClick={handleDailyButtonClick}>
                            Generate Daily Charts
                        </Button>
                    </Box>
                </Box>

                <Box marginTop={4}>
                    {isChart1Visible && generateWeeklyCharts(room1Id, room2Id, room1Name, room2Name, dataType)}
                    {isChart2Visible && generateDailyCharts(room1Id, room2Id, room1Name, room2Name, dataType)}
                </Box>






            </Box>
        </Box>
    );
};
export default DeviceLogSchedular;