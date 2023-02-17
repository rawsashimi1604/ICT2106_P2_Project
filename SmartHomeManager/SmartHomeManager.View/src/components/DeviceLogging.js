import React, { useState } from "react";
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

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

function DeviceLogging() {
    const [selectDevice, setSelectedDevice] = useState("");
    const [selectDevice2, setSelectedDevice2] = useState("");
    const [device1ID, setDevice1ID] = useState("");
    const [weeklyDevice1Log, setweeklyDevice1Log] = useState([]);
    const [allLog, setAllLog] = useState([]);
    const [test, setTest] = useState(0);

    const onChange = (event) => {
        const value = event.target.value;
        setDevice1ID(value);
    };

    const fetchWeeklyLog = async (id, date) => {
        try {
            //create the fetch request
            const res = await axios.get(`https://localhost:7140/api/DeviceLog/${id}/${date}`)

            //get the fetch request
            const data = await res.data
            setweeklyDevice1Log(data)

        } catch (err) {
            console.error(err)
        }
    }

    // displaying 
    return (
        <div>
            <h2>Device Log</h2>
            <br></br>
            <h2>Select first device</h2>
            <select onChange={onChange} className="form-select">
                <option defaultValue disabled>
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
                    fetchWeeklyLog(device1ID, "2023-02-15 13:49:50.7925408")
                }}
            > View weekly log </Button>

        </div>
    );
};
export default DeviceLogging;