import axios from "axios";
import React, { useState } from "react";
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
import WeeklyEnergyChart from "components/WeeklyEnergyChart";

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);



export default function DeviceLogging() {
    const [allDevices, setAllDevices] = useState({});
    //const [test, setTest] = useState(0); debug purpose
    const [Device1WeeklyEnergy, setDevice1WeeklyEnergy] = useState([])
    const [Device1WeeklyActvity, setDevice1WeeklyActivity] = useState([])
    //const [Device2WeeklyEnergy, setDevice2WeeklyEnergy] = useState([])
    //const [Device2WeeklyActvity, setDevice2WeeklyActivity] = useState([])
    const [device1ID, setDevice1ID] = useState("");
    //const [device2ID, setDevice2ID] = useState("");
 
    const onChange = (event) => {
        const value = event.target.value;
        setDevice1ID(value);
    };

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

    return (
        <div>
            Device Logging<br></br><br></br>
            
            <h2>Select first device</h2>
            <select onChange={onChange} className="form-select">
                <option disabled>
                    Select 1st device
                </option>
                <option value="385576BC-F97B-4B95-9B40-F423E4D16623"> Device 1</option>
                <option value="device2"> Device 2</option>
                <option value="device3"> Device 3</option>
            </select>
            <br></br>

            <Button
                variant="outlined"
                colorScheme='blue' 
                onClick={() => { 
                    fetchWeeklyLog(device1ID, "2023-02-13 00:00:00.0000000")
                }}
            > View weekly log </Button>
            {Device1WeeklyEnergy && <h2>{Device1WeeklyActvity}</h2>}
            <WeeklyEnergyChart
                Device1WeeklyEnergy={Device1WeeklyEnergy}
            />
        </div>
    );
}