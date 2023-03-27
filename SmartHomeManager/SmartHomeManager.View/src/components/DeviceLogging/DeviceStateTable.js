import React, { useState } from 'react';
import {
    Table,
    Thead,
    Tbody,
    Tr,
    Th,
    Td,
    TableContainer,
    Box,
} from '@chakra-ui/react';
import ActionButtons from './ActionButtons';

function DeviceStateTable({ devicesData, deviceLog }) {
    const [updatedLog, setUpdatedLog] = useState({});
    console.log(updatedLog)

    const DisplayData = devicesData?.map((device) => {
        const logData = deviceLog.find((log) => log.deviceId === device.deviceId);
        return (
            <Tr key={device.deviceId}>
                <Td>{device.deviceName}</Td>
                <Td>{logData?.deviceState}</Td>
                <Td>{logData?.totalUsage}</Td>
                <Td>{logData?.totalActivity}</Td>
                <ActionButtons id={device.deviceId} updateLog={setUpdatedLog} />
            </Tr>
        );
    });

    return (
        <Box>
            <TableContainer>
                <Table variant="striped" colorScheme="gray">
                    <Thead>
                        <Tr>
                            <Th>Device Name</Th>
                            <Th>Device State</Th>
                            <Th>Energy Usage</Th>
                            <Th>Activity</Th>
                            <Th>Actions</Th>
                        </Tr>
                    </Thead>
                    <Tbody>{DisplayData}</Tbody>
                </Table>
            </TableContainer>
        </Box>
    );
}

export default DeviceStateTable;
