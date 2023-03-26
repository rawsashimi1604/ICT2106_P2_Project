import { Button, Td, Text } from "@chakra-ui/react";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

function ActionButtons({ id, updateLog }) {
    const putLogOnStateOff = async () => {
        try {
            const res = await axios.put(`https://localhost:7140/api/DeviceLog/stateOff/${id}`);
            const logs = res.data;
            console.log(logs);
            updateLog(prevLogs => prevLogs.map(log => log.deviceId === id ? logs : log)); // update the state with the updated log data
        } catch (err) {
            console.error(err);
        }
    };

    const putLogOnStateOn = async () => {
        try {
            for (let i = 0; i < 7; i++) {
                const res = await axios.put(`https://localhost:7140/api/DeviceLog/stateOn/${id}`);
                const logs = res.data;
                console.log(logs);
                updateLog(prevLogs => prevLogs.map(log => log.deviceId === id ? logs : log)); // update the state with the updated log data
            }
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <Td>
            <Button colorScheme="green" marginRight="1rem" onClick={putLogOnStateOn}>
                Turn on
            </Button>
            <Button colorScheme="red" onClick={putLogOnStateOff}>
                Turn off
            </Button>
        </Td>
    );
}



export default ActionButtons;
