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
import EnergyEfficiencyService from "requests/services/EnergyEfficiencyService";
import ErrorWidget from "../widgets/ErrorWidget";
import LoadingWidget from "../widgets/LoadingWidget";
import { LazyMotion, domAnimation, AnimatePresence } from "framer-motion";

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
      borderWidth: 1,
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

export default function EnergyEfficiencyModal({ onClose, size, isOpen }) {
  const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";

  const [graphData, setGraphData] = useState(null);

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

  function getEnergyEfficiencyData() {
    setGraphData(null);
    onLoadingOpen();
    EnergyEfficiencyService.getEnergyEfficiencyData(SESSION_ACCOUNT_GUID)
      .then((response) => {
        const efData = response.data.data;
        const graph = {
          labels: [],
          datasets: [
            {
              label: "Energy Efficiency Index",
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
        onLoadingClose();
      })
      .catch((err) => {
        onLoadingClose();
        onErrorOpen();
      });
  }

  useEffect(() => {
    getEnergyEfficiencyData();
  }, []);

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
              <ModalHeader>Energy Efficiency Statistics</ModalHeader>
              <ModalCloseButton />
              <ModalBody>
                <Text>
                  Measure and optimize your device energy efficiency. Save
                  energy, reduce costs and environmental impact. We have
                  measured your smart devices against the national statistics.
                  This provides an overview of how efficient your device energy
                  usage is.
                </Text>
                
                <Text align="center" marginTop="10px" marginBottom="10px">The energy efficiency index ranges from 0 to 100, where a higher value indicates better energy efficiency and a lower value indicates poorer energy efficiency.</Text>
                <Flex justify="center" align="center">
                  <Flex
                    direction="column"
                    justify="center"
                    align="center"
                    width="80%"
                  >
                    {graphData && (
                      <Bar
                        // @ts-ignore
                        options={options}
                        data={graphData}
                      />
                    )}
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
