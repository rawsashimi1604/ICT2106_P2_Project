import React, { useState, useEffect } from "react";
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
import { Heading, Container, Text, Flex } from "@chakra-ui/react";
import EnergyEfficiencyService from "requests/services/EnergyEfficiencyService";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export const options = {
  indexAxis: "y",
  elements: {
    bar: {
      borderWidth: 2,
    },
  },
  responsive: true,
  plugins: {
    legend: {
      position: "right",
    },
    title: {
      display: true,
      text: "Energy Efficiency Index Graph.",
    },
  },
};

export function EfficiencyBar() {
  const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";
  const [graphData, setGraphData] = useState(null);

  function getEnergyEfficiencyData() {
    EnergyEfficiencyService.getEnergyEfficiencyData(SESSION_ACCOUNT_GUID).then(
      (response) => {
        const efData = response.data.data;
        const graph = {
          labels: [],
          datasets: [
            {
              label: "Dataset 1",
              data: [],
              borderColor: "rgb(255, 99, 132)",
              backgroundColor: "rgba(255, 99, 132, 0.5)",
            },
          ],
        };

        graph.datasets[0].data = efData.map((data) => {
          return Math.round(data.energyEfficiencyIndex * 100) / 100;
        });

        graph.labels = efData.map((data) => {
          return data.deviceName;
        });
        setGraphData(graph);
      }
    );
  }

  useEffect(() => {
    getEnergyEfficiencyData();
  }, [])


  return (
    <Flex direction="column" justify="center" align="center">
      <Heading pb={5}>Energy Efficiency</Heading>
      <Text pb={10}>We have measured your smart devices against the national statistics. This provides an overview of how efficient your device energy usage is.</Text>

      {
        graphData &&  
        <Bar
            // @ts-ignore
            options={options}
            data={graphData}
        />
      }

    </Flex>
  );
}
