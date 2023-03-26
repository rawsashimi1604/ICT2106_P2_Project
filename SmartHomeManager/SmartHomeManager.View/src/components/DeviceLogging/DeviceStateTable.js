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
    Text,
} from '@chakra-ui/react';
import ActionButtons from './ActionButtons';

function DeviceStateTable({ devicesData, deviceLog }) {
    const [updatedLog, setUpdatedLog] = useState([]);

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
            <Text>
                {updatedLog.map((log, index) => (
                    <Text key={index}>
                        {log.deviceId} {log.deviceState} {log.totalUsage} {log.totalActivity}
                    </Text>
                ))}
            </Text>
            <TableContainer>
                <Table variant="striped" colorScheme="gray">
                    <Thead>
                        <Tr>
                            <Th>Device Name</Th>
                            <Th>Device State</Th>
                            <Th>Energy Usage</Th>
                            <Th>Activity</Th>
                        </Tr>
                    </Thead>
                    <Tbody>{DisplayData}</Tbody>
                </Table>
            </TableContainer>
        </Box>
    );
}

export default DeviceStateTable;
