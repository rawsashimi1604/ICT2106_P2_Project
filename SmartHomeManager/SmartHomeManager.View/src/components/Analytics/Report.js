import React, { useState, useEffect } from 'react'
import {
    Select,
    Button,
    Flex,
    Container,
    Box,
    Heading,
    Text,


} from '@chakra-ui/react'
import {
    DownloadIcon,
} from '@chakra-ui/icons'

import ReportService from 'requests/services/ReportService'
// import { format } from 'date-fns';

function Report() {

    const [devices, setDevices] = useState([]);
    // const [account, setAccount] = useState([]);
    const [selectedValue, setSelectedValue] = useState('');
    const [selectedTimeframe, setSelectedTimeframe] = useState('');

    const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";



    const handleChange = (event) => {
        setSelectedValue(event.target.value);
    }

    const handleTimeChange = (event) => {
        setSelectedTimeframe(event.target.value);
        startDate = subtractMonths(startDate, event.target.value);
        setStartTime(format(startDate, 'yyyy-MM-dd kk:mm:ss'));
        setEndTime(format(endDate, 'yyyy-MM-dd kk:mm:ss'));
    }

    function getDevicesByGUID() {
        ReportService.getDevicesByGUID(SESSION_ACCOUNT_GUID)
            .then(response => {
                console.log(response.data)
                setDevices(response.data.data);
            }).catch(e => {
                console.log(e);
            })
    }

    useEffect(() => {
        getDevicesByGUID();
    }, [])

    const handleClick = () => {
        // handling the post request...
        ReportService.getDeviceReport(selectedValue, parseInt(selectedTimeframe))
            .then((response) => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                const current = new Date();
                const date = `${current.getDate()}${current.getMonth() + 1}${current.getFullYear()}`
                link.href = url;
                link.setAttribute('download', date + "-" + selectedValue + '-DeviceReport.pdf');
                document.body.appendChild(link);
                link.click();
            });
    }

    const handleHouseholdClick = () => {

        ReportService.getHouseholdReport(SESSION_ACCOUNT_GUID, parseInt(selectedTimeframe))
            .then((response) => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                const current = new Date();
                const date = `${current.getDate()}${current.getMonth() + 1}${current.getFullYear()}`
                link.href = url;
                link.setAttribute('download', date + "-" + SESSION_ACCOUNT_GUID + 'HouseholdReport.pdf');
                document.body.appendChild(link);
                link.click();
            });

        console.log("inside household report")

    }



    return (
        <>
            <Container mb="20px">
                <Flex align="center">
                    <Select placeholder='Select Timeframe' value={selectedTimeframe} onChange={handleTimeChange} >
                        <option value="1">Last 1 Month</option>
                        <option value="3">Last 3 Months</option>
                        <option value="6">Last 6 Months</option>
                    </Select>
                </Flex>
            </Container>

            <Container mb="20px">
                <Flex align="center">
                    <Text minW="150px">Device Report:</Text>
                    <Select placeholder='Select Device' value={selectedValue} onChange={handleChange} minW="450px">
                        {
                            devices?.map((device, i) => {
                                return (
                                    <option key={i} value={device.deviceID}>
                                        {device.deviceName} ({device.deviceID})
                                    </option>
                                )
                            })
                        }

                    </Select>
                    <Button ml={10} px={12} py={5} onClick={handleClick}>
                        <DownloadIcon />Download</Button>


                </Flex>
            </Container>

            <Container>
                <Flex align="center">

                    <Text minW="150px" mr="450px">Household Report:</Text>
                    <Button ml={10} px={12} py={5} onClick={handleHouseholdClick}>
                        <DownloadIcon />Download</Button>

                </Flex>

            </Container>
        </>
    )
}

export default Report