import React, { useState, useEffect } from "react";
import axios from "axios";
import { format } from "date-fns";
import { Box, Heading, Text } from "@chakra-ui/react";
import { Line } from "react-chartjs-2";

function DailyChart({ room1, room2, room1Name, room2Name, dataType }) {
    const [chartData, setChartData] = useState(null);
    const [room1HourlyData, setRoom1HourlyData] = useState([]);
    const [room2HourlyData, setRoom2HourlyData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const res = await axios.get(
                    `https://localhost:7140/api/DeviceLog/hourly/getByRoom/${room1}`
                );
                const data = await res.data;
                const [room1WeeklyEnergyUsage, room1HourlyActivityLevel] = data;

                const res2 = await axios.get(
                    `https://localhost:7140/api/DeviceLog/hourly/getByRoom/${room2}`
                );
                const [room2HourlyEnergyUsage, room2HourlyActivityLevel] = await res2.data;

                if (dataType === "energyUsage") {
                    setRoom1HourlyData(room1WeeklyEnergyUsage);
                    setRoom2HourlyData(room2HourlyEnergyUsage);
                } else {
                    setRoom1HourlyData(room1HourlyActivityLevel);
                    setRoom2HourlyData(room2HourlyActivityLevel);
                }
            } catch (error) {
                console.error(error);
            }
        };

        fetchData();
    }, [room1, room2, dataType]);

    useEffect(() => {
        if (room1HourlyData.length > 0 && room2HourlyData.length > 0) {
            const today = new Date();
            const hours = [...Array(24)].map((_, index) => {
                const date = new Date(today);
                date.setHours(index);
                return format(date, "HH:mm");
            });

            const chartData = {
                labels: hours,
                datasets: [
                    {
                        label: `${room1Name}`,
                        data: room1HourlyData,
                        fill: false,
                        borderColor: "rgb(75, 192, 192)",
                        tension: 0.1,
                    },
                    {
                        label: `${room2Name}`,
                        data: room2HourlyData,
                        fill: false,
                        borderColor: "rgb(192, 75, 192)",
                        tension: 0.1,
                    },
                ],
            };
            setChartData(chartData);
        }
    }, [room1HourlyData, room2HourlyData]);

    return (
        <Box borderWidth="1px" borderRadius="lg" overflow="hidden" p="4">
            <Heading size="md" mb="4">{`Hourly ${dataType} Comparison`}</Heading>
            {chartData ? (
                <Box>
                    <Line data={chartData} />
                </Box>
            ) : (
                <Text textAlign="center">Loading...</Text>
            )}
        </Box>
    );
}


export default DailyChart;
