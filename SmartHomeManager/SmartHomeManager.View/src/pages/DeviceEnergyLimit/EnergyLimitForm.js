import React, { useState } from 'react';
import { Button, ButtonGroup } from '@chakra-ui/react'
import { Input } from '@chakra-ui/react'

function EnergyLimitForm(props) {
    const [deviceId, setdeviceID] = useState('');
    const [deviceName, setdeviceName] = useState('');
    const [deviceBrand, setdeviceBrand] = useState('');
    const [deviceModel, setdeviceModel] = useState('');
    const [deviceTypeName, setdeviceTypeName] = useState('');
    const [deviceEnergyLimit, setdeviceEnergyLimit] = useState('');

    const changedeviceId = (event) => {
        setdeviceID(event.target.value);
    };

    const changedeviceName = (event) => {
        setdeviceName(event.target.value);
    };
    const changedeviceBrand = (event) => {
        setdeviceBrand(event.target.value);
    };
    const changedeviceModel = (event) => {
        setdeviceModel(event.target.value);
    };
    const changedeviceTypeName = (event) => {
        setdeviceTypeName(event.target.value);
    };
    const changedeviceEnergyLimit= (event) => {
        setdeviceEnergyLimit(event.target.value);
    };

    const transferValue = (event) => {
        event.preventDefault();
        const val = {
            deviceId,
            deviceName,
            deviceBrand,
            deviceModel,
            deviceTypeName,
            deviceEnergyLimit
        };
        props.func(val);
        clearState();
    };

    const clearState = () => {
        setdeviceID('');
        setdeviceName('');
        setdeviceBrand('');
        setdeviceModel('');
        setdeviceTypeName('');
        setdeviceEnergyLimit('');
    };
    return (
        <div>
            <div>Add limit for a device here</div>
            <br></br>
            <label>Device ID</label>
            <Input variant='filled' type="text" value={deviceId} onChange={changedeviceId} />
            <br></br>
            <label>Device Name</label>
            <Input variant='filled' type="text" value={deviceName} onChange={changedeviceName} />
            <br></br>
            <label>Device Brand</label>
            <Input variant='filled' type="text" value={deviceBrand} onChange={changedeviceBrand} />
            <br></br>
            <label>Device Model</label>
            <Input variant='filled' type="text" value={deviceModel} onChange={changedeviceModel} />
            <br></br>
            <label>Device Type Name</label>
            <Input variant='filled'type="text" value={deviceTypeName} onChange={changedeviceTypeName} />
            <br></br>
            <label>Device Energy Limit</label>
            <Input variant='filled'type="text" value={deviceEnergyLimit} onChange={changedeviceEnergyLimit} />
            <br></br>
            <br></br>
            <Button colorScheme='blue' onClick={transferValue}> Add Limit</Button>
        </div>
    );
}

export default EnergyLimitForm;