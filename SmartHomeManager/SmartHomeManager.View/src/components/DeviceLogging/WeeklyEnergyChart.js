import React, { useState, useEffect } from "react";
import {Box, Heading, Text, Select } from "@chakra-ui/react";
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

const options = {
    responsive: true,
    plugins: {
        legend: {
            position: "top",
        },
        title: {
            display: true,
            text: "Device Energy Usage Graph",
        },
    },
};


const weeklyLabel = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

const dailyLabel = ["0000", "0100", "0200", "0300", "0400", "0500", "0600", "0700", "0800", "0900", "1000", "1100", "1200", "1300", "1400", "1500", "1600", "1700", "1800", "1900", "2000", "2100", "2200", "2300"];


const weeklyActivityData = {
    labels: weeklyLabel,
    datasets: [
        {
            label: "Device 1",
            data: [50, 20, 30, 10, 50, 20, 60],
            backgroundColor: "rgba(255, 99, 132, 0.5)",
        },
        {
            label: "Device 2",
            data: [30, 40, 50, 60, 70, 80, 52],
            backgroundColor: "rgba(53, 162, 235, 0.5)",
        },
    ],


};


function WeeklyEnergyChart({ Device1WeeklyEnergy }) {

    const [DropDownValue, setDropDownValue] = useState(null);
    const [DropDownValue2, setDropDownValue2] = useState(null);
    const [chartData, setChartData] = useState(null);
    const [chartTwoData, setChartTwoData] = useState(weeklyActivityData);

    const weeklyData = {
        labels: weeklyLabel,
        datasets: [
            {
                label: "Device 1",
                data: [50, 20, 30, 50, 40, 60, 90],
                backgroundColor: "rgba(255, 99, 132, 0.5)",
            },
            {
                label: "Device 2",
                data: [40, 30, 70, 80, 10, 40, 65],
                backgroundColor: "rgba(75, 103, 230, 0.8)",
            },
        ],
    };

    const dailyData = {
        labels: dailyLabel,
        datasets: [
            {
                label: "Device 1",
                data: [50, 20, 30, 50, 40, 60, 90, 50, 20, 30, 50, 40, 60, 90, 50, 20, 30, 50, 40, 60, 90, 22, 88, 76],
                backgroundColor: "rgba(255, 99, 132, 0.5)",
            },
            {
                label: "Device 2",
                data: [40, 30, 70, 80, 10, 40, 65, 40, 60, 90, 50, 20, 30, 50, 20, 30, 50, 40, 60, 90, 50, 10, 87, 67],
                backgroundColor: "rgba(75, 103, 230, 0.8)",
            },
        ],
    };

    const dropDownHandler = (e) => {
        let value = e.target.value;
        setDropDownValue(value);
        if (value === "option1") {
              setChartData(weeklyData);
        }
        if (value === "option2") {
            setChartData(dailyData);
        }
    };

    return (
        <Box paddingTop="15">
            <Text> Select the chart to display</Text>
            <Select
                placeholder="Select Device Energy Usage View"
                onChange={(e) => dropDownHandler(e)}
                width={"200px"}
                variant='filled'

            >
                <option value="option1">Weekly</option>
                <option value="option2">Daily</option>

            </Select>
            {DropDownValue ? (
                <div style={{ width: "1300px" }}>
                    {Device1WeeklyEnergy && <Bar options={options} data={chartData} />}
                </div>
            ) : (
                <div>
                </div>
            )}


        </Box>


    )
}

export default WeeklyEnergyChart;