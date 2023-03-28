import React, { useState, useEffect } from "react";
import {Box, Heading, Text, Select } from "@chakra-ui/react";


function DeviceLogging() {


    const [deviceTypeName, setDeviceTypeName] = useState([]);
    const [selectedDevice, setSelectedDevice] = useState("");

    const [data, setData] = useState([]);
    const [selectedDeviceName1, setSelectedDeviceName1] = useState("");
    const [selectedDeviceName2, setSelectedDeviceName2] = useState("");


<<<<<<< Updated upstream
=======
    /*const [device1ID, setDevice1ID] = useState("");
    const [allDeviceID, setAllDevice1ID] = useState([]);
    const [Device1WeeklyEnergy, setDevice1WeeklyEnergy] = useState([])*/

>>>>>>> Stashed changes
    function fetchDeviceTypes() {
        fetch("https://localhost:7140/api/RegisterDevice/GetAllDeviceTypes/")
            .then((response) => response.json())
            .then((data) => setDeviceTypeName(data));
    }

    useEffect(() => {

        fetchDeviceTypes();

        if (selectedDevice !== "") {
            fetch(`https://localhost:7140/api/RegisterDevice/GetAllDevice/${selectedDevice}`)
                .then((response) => response.json())
                .then((data) => setData(data))
                .catch((error) => console.log(error));
        }
    }, [selectedDevice]);



    // displaying 
    return (
        <Box>
            <Heading alignContent="center">Device Log</Heading>
            <Text>This page displays charts to illustrate the energy usage and activity level of devices.</Text>
            <Box paddingTop="5">
                <text>Please select device to compare{'\n'}</text>
                <Select value={selectedDevice} onChange={(e) => setSelectedDevice(e.target.value)} placeholder="Please a select a device type">


                    {deviceTypeName.map(option => (
                        <option key={option} value={option}>{option}</option>
                    ))}

                </Select>

            </Box>


            <Box paddingTop="10" width="550px" display='inline-block'>           

                {selectedDevice && (

                    <><text>Please select the {selectedDevice} to compare{'\n'}</text>

                        <Select
                        value={selectedDeviceName1} onChange={(event) => { setSelectedDeviceName1(event.target.value); } }
                        placeholder={`Select 1st ${selectedDevice}`}
                        width={"500px"}
                        variant='filled'>

                        {data.filter(item => item !== selectedDeviceName2).map((item) => (
                            <option key={item} value={item}>{item}</option>
                        ))}


                        </Select></>

                )}

            </Box>

            <Box paddingTop="10" width="550px" display='inline-block'>
            
            {selectedDevice && (

                <Select
                    value={selectedDeviceName2} onChange={(e) => setSelectedDeviceName2(e.target.value)}
                    placeholder={`Select 2nd ${selectedDevice}`}
                    width={"500px"}
                    variant='filled'>

                    {data.filter(option => option !== selectedDeviceName1).map(option => (
                        <option key={option} value={option}>{option}</option>
                    ))}


                </Select>

            )}

            </Box>

<<<<<<< Updated upstream
=======
            {selectedDeviceName1 && selectedDeviceName2 && (

                <Text>Display Chart Here</Text>
                /*<WeeklyEnergyChart 
                    Device1WeeklyEnergy={Device1WeeklyEnergy}/> */
            )}

>>>>>>> Stashed changes
        </Box>

    );
};
export default DeviceLogging;