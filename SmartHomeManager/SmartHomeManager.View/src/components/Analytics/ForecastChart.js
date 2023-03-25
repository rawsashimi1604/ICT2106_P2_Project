import React, { useEffect, useState } from 'react'
import Chart from "chart.js/auto";
import { Bar, Line } from "react-chartjs-2";
import {
  Tabs,
  TabList,
  TabPanels,
  Tab,
  TabPanel,
  Box,
  Center,
  Container,
  Text,
  Heading,
  Flex,
  Select,
  Button
} from '@chakra-ui/react'; 
import ForecastService from 'requests/services/ForecastService';



function ForecastChart() {


  const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";

  const [graphData, setGraphData] = useState(null);
  const [costGraphData, setCostGraphData] = useState(null);
  const [selectedValue, setSelectedValue] = useState("");
  const [xLabel, setXLabel] = useState('');

  const options = {
    legend:{
      labels:{
        font:{
          size:30,
        }
      }
    },
    scales: {
        x: {
            beginAtZero: true,
            title:{
                display: true,
                text: xLabel,
                font:{
                  size: 20,
                  weight: "bold",
                }
            },
            ticks: {
                font: {
                    size: 20,
                },
            },
        },
        y:{
          beginAtZero: false,
          title:{
            display: true,
            text: "Forecasted Energy Usage (Watts)",
            font:{
              size: 20,
              weight: "bold",
            }
          },
          ticks: {
            max: 500000,
            font: {
                size: 20,
            }
          }
        },
  
    }
  };

  const options1 = {
    legend:{
      labels:{
        font:{
          size:30,
        }
      }
    },
    scales: {
        x: {
            beginAtZero: true,
            title:{
                display: true,
                text: xLabel,
                font:{
                  size: 20,
                  weight: "bold",
                }
            },
            ticks: {
                font: {
                    size: 20,
                },
            },
        },
        y:{
          beginAtZero: false,
          title:{
            display: true,
            text: "Forecasted Energy Usage Cost (SGD)",
            font:{
              size: 20,
              weight: "bold",
            }
          },
          ticks: {
            max: 500000,
            font: {
                size: 20,
            }
          }
        },
  
    }
  };

  

  const handleSelectChange = (event) => {
    setSelectedValue(event.target.value);
    if (event.target.value == 0){
      setXLabel('Dates')
    } else if(event.target.value == 1){
      setXLabel('Months')
    }else if(event.target.value == 2){
      setXLabel('Days')
    }
  }
    
  

  function getHouseholdForecast(){
    const current = new Date()
    ForecastService.getHouseholdForecast(SESSION_ACCOUNT_GUID, selectedValue)
      .then((response) =>{
        console.log(response)
        const forecastChartData = response.data.data;
        
        const graph = {
          labels: [],
          datasets: [
            {
              label: "Forecast For Following " + xLabel,
              backgroundColor: "rgb(255, 99, 132)",
              borderColor: "rgb(255, 99, 132)",
              data: [],
            },
          ],
        }

        const costGraph = {
          labels: [],
          datasets: [
            {
              label: "Forecast Cost For Following " + xLabel,
              backgroundColor: "rgb(255, 99, 132)",
              borderColor: "rgb(255, 99, 132)",
              data: [],
            },
          ],
        }
        
        graph.datasets[0].data = forecastChartData.map((data) => {
          return data.wattsValue
        })
        graph.labels = forecastChartData.map((data) => {
          return data.label
        })
        setGraphData(graph);

        

        costGraph.datasets[0].data = forecastChartData.map((data) => {
          return data.priceValue
        })
        costGraph.labels = forecastChartData.map((data) => {
          return data.label
        })
        setCostGraphData(costGraph)
      })
  }

  useEffect(() => {
    getHouseholdForecast()
  }, [selectedValue]);

  

  
  return (
    <Flex direction="column" justify="center" align="center">
      <Heading pb={5}>Forecast Household Energy Usage</Heading>
      <div>
        <Select value={selectedValue} onChange={handleSelectChange}>
          <option value="0">Following Month</option>
          <option value="1">Following Year</option>
          <option value="2">Following Week</option>
        </Select>
      </div>
      <Text pb={10}>This is your predicted total household energy usage. </Text>
        {graphData && <Line options={options} data={graphData} />}
      <Text pb={10} pt={20}>This is your predicted total household energy usage cost. </Text>
        {costGraphData && <Line options={options1} data={costGraphData} />}
    </Flex>
    )
  }

  
  export default ForecastChart