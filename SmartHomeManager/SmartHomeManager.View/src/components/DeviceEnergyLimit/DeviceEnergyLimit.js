import React, { useState, useEffect } from "react";
import { Button, Box, Heading, Text, Select } from "@chakra-ui/react";
import DeviceEnergyLimitService from "requests/services/DeviceEnergyLimitService";

function DeviceEnergyLimit() {
    const [deviceId, setDeviceId] = useState("");
    const [deviceIds, setDeviceIds] = useState([]);
    const [deviceEnergyLimit, setDeviceEnergyLimit] = useState();
    const [deviceEnergyLimits, setDeviceEnergyLimits] = useState([]);

    const onChange = (event) => {
        const value = event.target.value;
        setDeviceId(value);
        setDeviceEnergyLimit(
            deviceEnergyLimits.find((device) => device.deviceId === value)
        );
    };

    useEffect(() => {
        const fetchDeviceIds = async () => {
            try {
                const res = await fetch(
                    "https://localhost:7140/api/DeviceLog/{22222222-2222-2222-2222-222222222222}"
                );
                const data = await res.json();
                setDeviceIds(data);
            } catch (error) {
                console.error(error);
            }
        };
        fetchDeviceIds();

        const fetchDeviceEnergyLimits = async () => {
            try {
                const res = await DeviceEnergyLimitService.getAllDeviceEnergyLimit();
                setDeviceEnergyLimits(res.data);
            } catch (error) {
                console.error(error);
            }
        };
        fetchDeviceEnergyLimits();
    }, []);

    return (
        <Box>
            <Heading as="h2" alignContent="center">
                Device Energy Limit
            </Heading>
            <Text>This page displays energy limit of the select device.</Text>
            <Box paddingTop="5">
                <Text>Please select device to compare{"\n"}</Text>
                <Select onChange={onChange} defaultValue={""} variant="filled">
                    <option disabled={true} value="">
                        Please select device
                    </option>
                    {deviceIds.map((id, index) => (
                        <option key={id} value={id}>
                            {" "}
                            Device {index + 1}
                        </option>
                    ))}
                </Select>
            </Box>
            {deviceEnergyLimit && (
                <Heading as="h6" marginTop="5">
                    The device energy limit is {deviceEnergyLimit.deviceEnergyLimit}.
                </Heading>
            )}
        </Box>
    );
}

export default DeviceEnergyLimit;