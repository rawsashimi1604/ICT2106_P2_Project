import React from 'react'
import {
    Select,
    Button,
    Flex,
    Container,
} from '@chakra-ui/react'
import {
    DownloadIcon,
} from '@chakra-ui/icons'

import ReportService from 'requests/services/ReportService'

const handleClick = () => {
    // handling the post request...
    ReportService.getDeviceReport()
        .then((response) =>{
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            const current = new Date();
            const date = `${current.getDate()}${current.getMonth()+1}${current.getFullYear()}`
            link.href = url;
            link.setAttribute('download', date + '.pdf');
            document.body.appendChild(link);
            link.click();
    });
}

function EfficiencyTable() {
    return (
        <>
            <Container>
                <Flex>
                    <Select placeholder='Select Device'>
                        <option value='option1'>Device 1</option>
                        <option value='option2'>Device 2</option>
                        <option value='option3'>Device 3</option>
                    </Select>
                    <Button ml={10} px={12} py={5} onClick={handleClick}>
                        <DownloadIcon mr={4} />Download</Button>
                </Flex>
            </Container>
        </>
    )
}

export default EfficiencyTable