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
        ],
    };

    useEffect(() => {
        // If your API returns some device weekly data, then set the chart data...
        if (Device1WeeklyEnergy) {
            setChartData({
                labels: weeklyLabel,
                datasets: [
                    {
                        label: "Device 1",
                        data: Device1WeeklyEnergy,
                        backgroundColor: "rgba(255, 99, 132, 0.5)",
                    },
                ],
            })
        }
    }, [Device1WeeklyEnergy])

    const dropDownHandler = (e) => {
        let value = e.target.value;
        setDropDownValue(value);
        /*  if (value === "option1") {
              setChartData(weeklyData);
          } */
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