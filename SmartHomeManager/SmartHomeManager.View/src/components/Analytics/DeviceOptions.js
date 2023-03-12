import React, { useState, useEffect } from "react";
import { Spinner, Flex, Select } from "@chakra-ui/react";

export default function DeviceOptions({ devices }){

    const SPINNER_TIMEOUT = 2000;
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    // On component load
    useEffect(() => {
        setTimeout(() => setIsDataLoaded(true), SPINNER_TIMEOUT)
    }, [])

    return(
        <>
            <Select placeholder='Select Device'>
            {
                devices && isDataLoaded ?
                devices.map((device, i) => {
                    return(
                        <option key={i} value={device.deviceID}>
                            device.deviceID
                        </option>
                    )
                }) :
                <Spinner alignSelf="center" my="4" />
            }
            </Select>
        </>
    )

}