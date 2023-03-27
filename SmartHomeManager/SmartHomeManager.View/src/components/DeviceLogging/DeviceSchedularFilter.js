import React, { useState, useEffect } from 'react';
import axios from 'axios';

import {
    Text,
    Box,
    Select
} from '@chakra-ui/react';

import DeviceStateTable from 'components/DeviceLogging/DeviceStateTable';

function DeviceSchedulerFilter({ room }) {
    const [dailyDeviceLog, setDailyDeviceLog] = useState([]);
    const [weeklyDeviceLog, setWeeklyDeviceLog] = useState([]);
    const [allDevices, setAllDevices] = useState([]);
    const [selectedOption, setSelectedOption] = useState('Weekly');

    const getDailyLogs = async (deviceId) => {
        try {
            const res = await axios.get(
                `https://localhost:7140/api/DeviceLog/daily/${deviceId}`
            );
            console.log(res.data);
            return res.data;
        } catch (error) {
            console.log(error);
        }
    };

    const getWeeklyLogs = async (deviceId) => {
        try {
            const res = await axios.get(
                `https://localhost:7140/api/DeviceLog/weekly/${deviceId}`
            );
            return res.data;
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        const getDevicesFromRoom = async () => {
            try {
                const res = await axios.get(
                    `https://localhost:7140/api/DeviceLog/getDevices/${room}`
                );
                const devices = res.data;
                setAllDevices(devices);
            } catch (error) {
                console.log(error);
            }
        };
        if (room) {
            getDevicesFromRoom();
        }
    }, [room, selectedOption]); // add selectedOption as a dependency

    useEffect(() => {
        const getLogs = async () => {
            if (selectedOption === 'Daily' && allDevices.length > 0) {
                const dailyLogs = [];
                for (let i = 0; i < allDevices.length; i++) {
                    const logs = await getDailyLogs(allDevices[i].deviceId);
                    dailyLogs.push(logs);
                }
                setDailyDeviceLog(dailyLogs);
                setWeeklyDeviceLog([]);
            } else if (selectedOption === 'Weekly' && allDevices.length > 0) {
                const weeklyLogs = [];
                for (let i = 0; i < allDevices.length; i++) {
                    const logs = await getWeeklyLogs(allDevices[i].deviceId);
                    weeklyLogs.push(logs);
                }
                setWeeklyDeviceLog(weeklyLogs);
                setDailyDeviceLog([]);
            }
        };
        if (selectedOption !== '') {
            getLogs();

        }
    }, [selectedOption,allDevices]); // add selectedOption and allDevices as dependencies

    const handleOptionSelect = async (event) => {
        const value = event.target.value;
        setSelectedOption(value);
    };

    return (
        <Box>
            <Text>Select daily or weekly table</Text>
            <Select value={selectedOption} onChange={handleOptionSelect} mb="1em">
                <option disabled={true} value="">
                    Select an option
                </option>
                <option value="Weekly">Weekly</option>
                <option value="Daily">Daily</option>
            </Select>

            {selectedOption === "Weekly" && weeklyDeviceLog.length > 0 && (
                <DeviceStateTable devicesData={allDevices} deviceLog={weeklyDeviceLog} />
            )}
            {selectedOption === "Daily" && dailyDeviceLog.length > 0 && (
                <DeviceStateTable devicesData={allDevices} deviceLog={dailyDeviceLog} />
            )}
        </Box>
    );


}


export default DeviceSchedulerFilter;