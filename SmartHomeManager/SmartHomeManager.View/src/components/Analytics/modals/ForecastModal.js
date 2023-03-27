import React, { useEffect, useState } from "react";
import Chart from "chart.js/auto";
import { Bar, Line } from "react-chartjs-2";
import {
  Tabs,
  TabList,
  TabPanels,
  Tab,
  TabPanel,
  Box,
  Select,
  Center,
  Container,
  Text,
  Heading,
  Flex,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  Button,
  useDisclosure,
} from "@chakra-ui/react";
import ForecastService from "requests/services/ForecastService";
import ErrorWidget from "../widgets/ErrorWidget";
import LoadingWidget from "../widgets/LoadingWidget";
import { LazyMotion, domAnimation, AnimatePresence } from "framer-motion";

function ForecastModal({ onClose, size, isOpen }) {
  const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";

  const [graphData, setGraphData] = useState(null);
  const [costGraphData, setCostGraphData] = useState(null);
  const [selectedValue, setSelectedValue] = useState("");
  const [xLabel, setXLabel] = useState("");

  const {
    isOpen: isErrorOpen,
    onOpen: onErrorOpen,
    onClose: onErrorClose,
  } = useDisclosure();

  const {
    isOpen: isLoadingOpen,
    onOpen: onLoadingOpen,
    onClose: onLoadingClose,
  } = useDisclosure();

  const handleSelectChange = (event) => {
    setSelectedValue(event.target.value);
    if (event.target.value == 0) {
      setXLabel("Dates");
    } else if (event.target.value == 1) {
      setXLabel("Months");
    } else if (event.target.value == 2) {
      setXLabel("Days");
    }
  };

  const options = {
    legend: {
      labels: {
        font: {
          size: 30,
        },
      },
    },
    scales: {
      x: {
        beginAtZero: true,
        title: {
          display: true,
          text: xLabel,
          font: {
            size: 20,
            weight: "bold",
          },
        },
        ticks: {
          font: {
            size: 20,
          },
        },
      },
      y: {
        beginAtZero: false,
        title: {
          display: true,
          text: "Forecasted Energy Usage (Watts)",
          font: {
            size: 20,
            weight: "bold",
          },
        },
        ticks: {
          max: 500000,
          font: {
            size: 20,
          },
        },
      },
    },
  };

  const options1 = {
    legend: {
      labels: {
        font: {
          size: 30,
        },
      },
    },
    scales: {
      x: {
        beginAtZero: true,
        title: {
          display: true,
          text: xLabel,
          font: {
            size: 20,
            weight: "bold",
          },
        },
        ticks: {
          font: {
            size: 20,
          },
        },
      },
      y: {
        beginAtZero: false,
        title: {
          display: true,
          text: "Forecasted Energy Usage Cost (SGD)",
          font: {
            size: 20,
            weight: "bold",
          },
        },
        ticks: {
          max: 500000,
          font: {
            size: 20,
          },
        },
      },
    },
  };

  function getHouseholdForecast() {
    setGraphData(null);
    setCostGraphData(null);
    onLoadingOpen();
    const current = new Date();
    ForecastService.getHouseholdForecast(SESSION_ACCOUNT_GUID, selectedValue)
      .then((response) => {
        console.log(response);
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
        };

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
        };

        graph.datasets[0].data = forecastChartData.map((data) => {
          return data.wattsValue;
        });
        graph.labels = forecastChartData.map((data) => {
          return data.label;
        });
        setGraphData(graph);

        costGraph.datasets[0].data = forecastChartData.map((data) => {
          return data.priceValue;
        });
        costGraph.labels = forecastChartData.map((data) => {
          return data.label;
        });
        setCostGraphData(costGraph);
        onLoadingClose();
      })
      .catch((err) => {
        onLoadingClose();
        onErrorOpen();
      });
  }

  useEffect(() => {
    getHouseholdForecast();
  }, [selectedValue]);

  return (
    <>
      <LazyMotion features={domAnimation}>
        <AnimatePresence>
          <Modal onClose={onClose} size={size} isOpen={isOpen}>
            <ErrorWidget
              onClose={onErrorClose}
              size="xl"
              isOpen={isErrorOpen}
            />

            <LoadingWidget
              onClose={onLoadingClose}
              size="xl"
              isOpen={isLoadingOpen}
            />
            <ModalOverlay />
            <ModalContent>
              <ModalHeader>Energy Usage Forecast</ModalHeader>
              <ModalCloseButton />
              <ModalBody>
                <Text>
                  Make informed decisions with our energy usage forecast
                  feature. Our advanced analytics tools allows you to predict
                  your consumption. In addition, our platform forecasts energy
                  prices, giving you the ability to plan and budget accordingly.
                </Text>

                <Flex
                  direction="column"
                  justify="center"
                  align="start"
                  marginTop="20px"
                  gap="20px"
                >
                  <div>
                    <Select value={selectedValue} onChange={handleSelectChange}>
                      <option value="-1">Please select a timeframe</option>
                      <option value="0">Following Month</option>
                      <option value="1">Following Year</option>
                      <option value="2">Following Week</option>
                    </Select>
                  </div>
                  <Flex flexDirection="row" gap="10px" w="100%">
                    <Flex flexDirection="column" gap="10px" w="50%">
                      <Text>Forecasted household energy usage.</Text>
                      {graphData && <Line options={options} data={graphData} />}
                    </Flex>

                    <Flex flexDirection="column" gap="10px" w="50%">
                      <Text>Forecasted household energy usage cost.</Text>
                      {costGraphData && (
                        <Line options={options1} data={costGraphData} />
                      )}
                    </Flex>
                  </Flex>
                </Flex>
              </ModalBody>
              <ModalFooter>
                <Button onClick={onClose}>Close</Button>
              </ModalFooter>
            </ModalContent>
          </Modal>
        </AnimatePresence>
      </LazyMotion>
    </>
  );
}

export default ForecastModal;
