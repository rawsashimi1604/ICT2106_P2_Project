import React, { useState, useEffect } from "react";
import axios from "axios";
import { Button, Select } from "@chakra-ui/react";
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from "chart.js";
import { Bar } from "react-chartjs-2";
import WeeklyEnergyChart from "components/DeviceLogging/WeeklyEnergyChart";


ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

function DeviceLogging() {
    const [device1ID, setDevice1ID] = useState("");
    const [allDeviceID, setAllDevice1ID] = useState([]);
    const [Device1WeeklyEnergy, setDevice1WeeklyEnergy] = useState([])


    const onChange = (event) => {
        const value = event.target.value;
        setDevice1ID(value);
    };

    useEffect(() => {
        const fetchDeviceID = async () => {
            try {
                const res = await fetch('https://localhost:7140/api/DeviceLog/{22222222-2222-2222-2222-222222222222}', {
                    method: 'GET',
                    headers: {
                        accept: 'text/plain',
                    },
                })
                const data = await res.json()
                setAllDevice1ID(data)
            } catch (error) {
                console.error(error)
            }
        }
        fetchDeviceID()
    }, [])

    const fetchWeeklyLog = async (id, date) => {
        try {
            // loops 7 times to get weekly log
            //
            // var dateAndTime = fetechedDateTimeVariable

            // var date = dateAndTime.Date
            const dateString = date.split(" ")
            // date = 2023-02-15
            const day = dateString[0].split("-")
            // 15 should be in dateOfDay
            let dateOfDay = parseInt(day[2])


            for (let i = 0; i < 7; i++) {

                let getWeekData = day[0] + "-" + day[1] + "-" + dateOfDay + " 00:00:00.0000000"

                //create the fetch request
                const res = await axios.get(`https://localhost:7140/api/DeviceLog/${id}/${getWeekData}`)
                console.log(res)
                dateOfDay = dateOfDay + 1
                // result from backend
                let energyUsage = await res.data[0]
                console.log(energyUsage)
                const activity = await res.data[1]

                setDevice1WeeklyEnergy(current => [...current, energyUsage])

            }
            //get the fetch request

            // add weekly energy log from res according to deviceID
            /*            if (id != device1ID) {
                            if (Device2WeeklyEnergy == null) {
                                setDevice2WeeklyEnergy(data)
                            } else {
                                //append
                                setDevice2WeeklyEnergy(current => [...current, ...data])
                            }
                        } else {
                            if (Device1WeeklyEnergy == null) {
                                setDevice1WeeklyEnergy(data)
                            } else {
                                //append
                                setDevice1WeeklyEnergy(current => [...current, ...data])
                            }
                        }
            
                        // add weekly activity log from res according to deviceID
                        if (id != device1ID) {
                            if (Device2WeeklyEnergy == null) {
                                setDevice2WeeklyActivity(data)
                            } else {
                                //append
                                setDevice2WeeklyActivity(current => [...current, ...data])
                            }
                        } else {
                            if (Device1WeeklyEnergy == null) {
                                setDevice1WeeklyActivity(data)
                            } else {
                                //append
                                setDevice1WeeklyActivity(current => [...current, ...data])
                            }
                        }*/

        } catch (err) {
            console.error(err)
        }


    }

    // displaying 
    return (
        <>
            <h2>Device Log</h2>
            <br></br>
            <h2>Select first device</h2>
            <select
                onChange={onChange}
                defaultValue={""}
            >
                <option disabled={true} value="">Please select device</option>
                {
                    allDeviceID.map((list, index) =>
                        <option key={index} value={list}> Device {index}</option>)
                }
            </select>
            <br></br>

            <Button
                variant="outlined"
                colorScheme='blue'
                onClick={() => {
                    fetchWeeklyLog(device1ID, "2023-02-13 00:00:00")
                }}
            > View weekly log </Button>
            <WeeklyEnergyChart
                Device1WeeklyEnergy={Device1WeeklyEnergy}
            />
        </>
    );
};
export default DeviceLogging;