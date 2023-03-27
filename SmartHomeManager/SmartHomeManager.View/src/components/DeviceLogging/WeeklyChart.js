import React, { useState, useEffect } from "react";
import axios from "axios";
import { format } from "date-fns";
import { Box, Heading, Text } from "@chakra-ui/react";
import { Line } from "react-chartjs-2";

function WeeklyChart({ room1, room2, room1Name, room2Name, dataType }) {
    const [chartData, setChartData] = useState(null);
    const [room1WeeklyData, setRoom1WeeklyData] = useState([]);
    const [room2WeeklyData, setRoom2WeeklyData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const res = await axios.get(
                    `https://localhost:7140/api/DeviceLog/weekly/getByRoom/${room1}`
                );
                const data = await res.data;
                const [room1WeeklyEnergyUsage, room1WeeklyActivityLevel] = data;

                const res2 = await axios.get(
                    `https://localhost:7140/api/DeviceLog/weekly/getByRoom/${room2}`
                );
                const [room2WeeklyEnergyUsage, room2WeeklyActivityLevel] = await res2.data;

                if (dataType === "energyUsage") {
                    setRoom1WeeklyData(room1WeeklyEnergyUsage);
                    setRoom2WeeklyData(room2WeeklyEnergyUsage);
                } else {
                    setRoom1WeeklyData(room1WeeklyActivityLevel);
                    setRoom2WeeklyData(room2WeeklyActivityLevel);
                }
            } catch (error) {
                console.error(error);
            }
        };

        fetchData();
    }, [room1, room2, dataType]);

    useEffect(() => {
        if (room1WeeklyData.length > 0 && room2WeeklyData.length > 0) {
            const timestamps = room1WeeklyData.map((_, index) => {
                const date = new Date();
                date.setDate(date.getDate() - 6 + index);
                return format(date, "dd MMM");
            });

            const chartData = {
                labels: timestamps,
                datasets: [
                    {
                        label: `${room1Name}`,
                        data: room1WeeklyData,
                        fill: false,
                        borderColor: "rgb(75, 192, 192)",
                        tension: 0.1,
                    },
                    {
                        label: `${room2Name}`,
                        data: room2WeeklyData,
                        fill: false,
                        borderColor: "rgb(192, 75, 192)",
                        tension: 0.1,
                    },
                ],
            };
            setChartData(chartData);
        }
    }, [room1WeeklyData, room2WeeklyData]);

    return (
        <Box borderWidth="1px" borderRadius="lg" overflow="hidden" p="4">
            <Heading size="md" mb="4">{`Weekly ${dataType} Comparison`}</Heading>
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


export default WeeklyChart;
