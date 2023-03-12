import React, { useState, useEffect } from 'react'
import {
    Select,
    Button,
    Flex,
    Container,
    Box,
} from '@chakra-ui/react'
import {
    DownloadIcon,
} from '@chakra-ui/icons'

import ReportService from 'requests/services/ReportService'




function Report() {

    const [devices, setDevices] = useState([]);
    const [selectedValue, setSelectedValue] = useState('');

    const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";

    const handleChange = (event) => {
        setSelectedValue(event.target.value);
      }

    function getDevicesByGUID(){
        ReportService.getDevicesByGUID(SESSION_ACCOUNT_GUID)
            .then(response => {
                console.log(response.data)
                setDevices(response.data.data);
            }).catch(e =>{
                console.log(e);
            })
    }

    useEffect(() => {
        getDevicesByGUID();
    }, [])

    const handleClick = () => {
        // handling the post request...
        ReportService.getDeviceReport(selectedValue)
            .then((response) =>{
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                const current = new Date();
                const date = `${current.getDate()}${current.getMonth()+1}${current.getFullYear()}`
                link.href = url;
                link.setAttribute('download', date + "-" + selectedValue + '-Report.pdf');
                document.body.appendChild(link);
                link.click();
        });
    }
    
    return (
        <>
            <Container>
                <Flex>
                    <Select placeholder='Select Device' value={selectedValue} onChange={handleChange}>
                        {
                            devices?.map((device,i) =>{
                                return(
                                    <option key={i} value={device.deviceID}>
                                        {device.deviceName} ({device.deviceID})
                                    </option>
                                )
                            })
                        }

                    </Select>

                    <Button ml={10} px={12} py={5} onClick={handleClick}>
                        <DownloadIcon mr={4} />Download</Button>
                </Flex>
            </Container>
        </>
    )
}

export default Report