import {
    Button,
    Table,
    Thead,
    Tbody,
    Tfoot,
    Tr,
    Th,
    Td,
    TableContainer,
    Box
} from '@chakra-ui/react'
import React from "react";
import Buttons from "./Buttons"

function jsonToTable({ ruleData }) {
    console.log(ruleData);
    const DisplayData = ruleData.map((info) => {
        return (
            <Tr key={info.ruleId}>
                <Td>{info.ruleName}</Td>
                <Td>{info.deviceId}</Td>
                <Td>{info.startTime}</Td>
                <Td>{info.startTime}</Td>
                <Td>{info.startTime}</Td>
                <Td>{info.startTime}</Td>
                <Td>{info.startTime}</Td>
                <Td>{info.startTime}</Td>
                <Buttons props={info} />
            </Tr>
        );
    });
    return (
        <TableContainer w='100%'>
            <Table variant='striped' colorScheme='gray' maxWidth='100%'>
                <Thead>
                    <Tr>
                        <Th>Device Name </Th>
                        <Th>Device Brand </Th>
                        <Th>Device Model</Th>
                        <Th>DeviceType Name</Th>
                        <Th>Device Energy Limits</Th>
                        <Th>Room Name</Th>
                        <Th>Account Name</Th>
                        <Th>Profile Name</Th>
                    </Tr>
                </Thead>
                <Tbody>{DisplayData}</Tbody>
            </Table>
        </TableContainer>
    );
}

export default jsonToTable;
