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
import DeviceOptions from './DeviceOptions'

const handleClick = () => {
    // handling the post request...
    ReportService.getDeviceReport()
        .then((response) =>{
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            const current = new Date();
            const date = `${current.getDate()}${current.getMonth()+1}${current.getFullYear()}`
            link.href = url;
            link.setAttribute('download', date + 'DeviceReport.pdf');
            document.body.appendChild(link);
            link.click();
    });
}

function Report() {

    const [devices, setDevices] = useState([]);

    function getDevicesByGUID(){
        ReportService.getDevicesByGUID()
            .then(response => {
                console.log(response.data)
                setDevices(response.data.devicesObject);
            }).catch(e =>{
                console.log(e);
            })
    }

    useEffect(() => {
        getDevicesByGUID();
    }, [])
    
    return (
        <>
            <Container>
                <Flex>
                    {/* <Select placeholder='Select Device'>
                        {
                            devices?.map((device,i) =>{
                                console.log(i)
                                return(
                                    <option key={i} value={device.deviceID}>
                                        {device.deviceID}
                                    </option>
                                )
                            })
                        }

                    </Select> */}

                    <DeviceOptions devices = {devices}/>

                    <Button ml={10} px={12} py={5} onClick={handleClick}>
                        <DownloadIcon mr={4} />Download</Button>
                </Flex>
            </Container>
        </>
    )
}

export default Report