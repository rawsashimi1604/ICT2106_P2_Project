import React, { useState, useEffect } from 'react';
import axios from 'axios';

import {
    Tabs,
    TabList,
    TabPanels,
    Tab,
    TabPanel,
    Text,
    Box,
    Select
} from '@chakra-ui/react';

import DeviceStateTable from 'components/DeviceLogging/DeviceStateTable';

function DeviceSchedulerFilter({ room }) {
    const [dailyDeviceLog, setDailyDeviceLog] = useState([]);
    const [weeklyDeviceLog, setWeeklyDeviceLog] = useState([]);
    const [allDevices, setAllDevices] = useState([]);
    const [activeTab, setActiveTab] = useState(0);
    const [selectedOption, setSelectedOption] = useState('Weekly');

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
    }, [room]);

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


    const handleOptionSelect = async (event) => {
        const value = event.target.value;
        setSelectedOption(value);

        if (value === 'Daily') {
            const dailyLogs = await getDailyLogs('33333333-3333-3333-3333-333333333333');
            setDailyDeviceLog([dailyLogs]);
            setWeeklyDeviceLog([]);
        } else if (value === 'Weekly') {
            const weeklyLogs = [];
            for (let i = 0; i < allDevices.length; i++) {
                const logs = await getWeeklyLogs(allDevices[i]);
                weeklyLogs.push(logs);
            }
            setWeeklyDeviceLog(weeklyLogs);
            setDailyDeviceLog([]);
        }
    };

    return (
        <Box>
            <Select value={selectedOption} onChange={handleOptionSelect} mb="1em">
                <option value="Weekly">Weekly</option>
                <option value="Daily">Daily</option>
            </Select>
            {selectedOption === 'Weekly' && (
                <DeviceStateTable devicesData={allDevices} deviceLog={weeklyDeviceLog}  />
            )}
            {selectedOption === 'Daily' && (
                <DeviceStateTable devicesData={allDevices} deviceLog={dailyDeviceLog}  />
            )}
            {dailyDeviceLog.map((log, index) =>
                <Text key={index}> {log.deviceId} {log.deviceState} {log.totalUsage} { log.totalActivity}</Text>
            )
            }
        </Box>
    );
}

export default DeviceSchedulerFilter;