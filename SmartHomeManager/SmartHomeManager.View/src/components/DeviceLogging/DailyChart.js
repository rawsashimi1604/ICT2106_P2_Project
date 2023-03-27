import React, { useState, useEffect } from "react";
import axios from "axios";
import { format } from "date-fns";
import { Box, Heading, Text } from "@chakra-ui/react";
import { Line } from "react-chartjs-2";

function DailyChart({ room1, room2, dataType }) {
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
                            label: `${room1}`,
                            data: room1HourlyData,
                            fill: false,
                            borderColor: "rgb(75, 192, 192)",
                            tension: 0.1,
                        },
                        {
                            label: `${room2}`,
                            data: room2HourlyData,
                            fill: false,
                            borderColor: "rgb(192, 75, 192)",
                            tension: 0.1,
                        },
                    ],
                };
                setChartData(chartData);
            } catch (error) {
                console.error(error);
            }
        };

        fetchData();
    }, [room1, room2, dataType]);

    return (
        <Box borderWidth="1px" borderRadius="lg" overflow="hidden" p="4">
            <Heading size="md" mb="4">{`Hourly ${dataType} Comparison`}</Heading>
            {chartData ? (
                <div>
                    <Line data={chartData} />
                    <div>
                        {chartData.datasets.map((dataset, index) => (
                            <div key={index}>
                                <h2>{dataset.label}</h2>
                                {dataset.data.map((data, index) => (
                                    <p key={index}>
                                        Hour {index}: {data}
                                    </p>
                                ))}
                            </div>
                        ))}
                    </div>
                </div>
            ) : (
                <Text textAlign="center">Loading...</Text>
            )}
        </Box>
    );
}

export default DailyChart;
