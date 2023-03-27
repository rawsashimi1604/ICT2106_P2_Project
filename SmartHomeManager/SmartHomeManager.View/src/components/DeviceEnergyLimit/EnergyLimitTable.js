import React, { useState } from 'react';
import EnergyLimitForm from './EnergyLimitForm';
import EnergyLimitData from './EnergyLimit.json';
import {
    Table,
    Thead,
    Tbody,
    Tfoot,
    Tr,
    Th,
    Td,
    TableCaption,
    TableContainer,
} from '@chakra-ui/react'

function EnergyLimitTable() {
    const [energyLimitData, setenergyLimitData] = useState(EnergyLimitData);

    const tableRows = energyLimitData.map((info) => {
        return (
            <tr key={info.id}>
                <td>{info.deviceId}</td>
                <td>{info.deviceName}</td>
                <td>{info.deviceBrand}</td>
                <td>{info.deviceModel}</td>
                <td>{info.deviceTypeName}</td>
                <td>{info.deviceEnergyLimit}</td>
            </tr>
        );
    });
    const addRows = (data) => {
        const totalData = energyLimitData.length;
        data.id = totalData + 1;
        const updatedEnergyLimitData = [...energyLimitData];
        updatedEnergyLimitData.push(data);
        setenergyLimitData(updatedEnergyLimitData);
    };
    return (
        <div>
            <TableContainer>
                <Table variant='striped' colorScheme='teal'>
                    <TableCaption>List of Energy Limits</TableCaption>
                    <Thead>
                        <Tr>
                            <Th>Device ID</Th>
                            <Th>Device Name</Th>
                            <Th>Device Brand</Th>
                            <Th>Device Model</Th>
                            <Th>Device Type Name</Th>
                            <Th>Device Limit</Th>
                        </Tr>
                    </Thead>
                    <Tbody>{tableRows}</Tbody>
                </Table>

            </TableContainer>
            <EnergyLimitForm func={addRows}/>
        </div>
    );
}
export default EnergyLimitTable;