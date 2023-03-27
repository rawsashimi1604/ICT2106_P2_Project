import React, { useState, useEffect } from "react";
import {
  Flex,
  Card,
  CardBody,
  Text,
  Heading,
  Box,
  useDisclosure,
} from "@chakra-ui/react";

import { ReactComponent as WeatherSvg } from "./assets/Weather.svg";
import { ReactComponent as PlantSvg } from "./assets/Plant.svg";
import { ReactComponent as ReportSvg } from "./assets/Report.svg";

import CarbonChartModal from "./modals/CarbonChartModal";
import ForecastModal from "./modals/ForecastModal";
import EnergyEfficiencyModal from "./modals/EnergyEfficiencyModal";
import ReportModal from "./modals/ReportModal";

function Dashboard() {
  const {
    isOpen: isCarbonFootprintOpen,
    onOpen: onCarbonFootprintOpen,
    onClose: onCarbonFootprintClose,
  } = useDisclosure();

  const {
    isOpen: isForecastOpen,
    onOpen: onForecastOpen,
    onClose: onForecastClose,
  } = useDisclosure();

  const {
    isOpen: isEnergyEfficiencyOpen,
    onOpen: onEnergyEfficiencyOpen,
    onClose: onEnergyEfficiencyClose,
  } = useDisclosure();

  const {
    isOpen: isReportOpen,
    onOpen: onReportOpen,
    onClose: onReportClose,
  } = useDisclosure();

  return (
    <>
      {/* CarbonFootprintModal */}
      <CarbonChartModal
        onClose={onCarbonFootprintClose}
        size="full"
        isOpen={isCarbonFootprintOpen}
      />

      <ForecastModal
        onClose={onForecastClose}
        size="full"
        isOpen={isForecastOpen}
      />

      <EnergyEfficiencyModal
        onClose={onEnergyEfficiencyClose}
        size="full"
        isOpen={isEnergyEfficiencyOpen}
      />

      <ReportModal onClose={onReportClose} size="xl" isOpen={isReportOpen} />

      {/* Dashboard */}
      <Flex flexDirection="column" gap="20px">
        <Flex flexDirection="row" gap="20px">
          {/* Welcome */}
          <Card w="50%" boxShadow="md">
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px">
                  <Heading>Welcome John Tan,</Heading>
                  <Text>Monday, 27 March 2023</Text>
                  <Text>
                    Get valuable insights on your smart devices with our
                    analytics dashboard. Monitor usage patterns and identify
                    energy-saving opportunities. Take control of your energy
                    consumption and make smarter choices for your home.
                  </Text>
                </Flex>

                <Flex>
                  <WeatherSvg />
                </Flex>
              </Flex>
            </CardBody>
          </Card>

          {/* Carbon footprint */}
          <Card
            w="50%"
            onClick={() => onCarbonFootprintOpen()}
            cursor="pointer"
            boxShadow="md" // Add box shadow
            transition="box-shadow 0.2s ease-in-out" // Add transition effect
            _hover={{
              boxShadow: "lg", // Add larger box shadow on hover
            }}
          >
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px" maxWidth="80%">
                  <Heading fontSize="30px">Carbon Footprint</Heading>
                  <Text>
                    Carbon Footprint: Compare the energy usage of your household
                    to national household statistics, measured in Watts. See the
                    environmental impact of your Smart Devices.
                  </Text>
                  <Text>This months usage: 500W</Text>
                </Flex>

                <Flex>
                  <Box pos="absolute" top="47px" right="30px">
                    <PlantSvg />
                  </Box>
                </Flex>
              </Flex>
            </CardBody>
          </Card>
        </Flex>

        <Flex flexDirection="row" gap="20px">
          {/* Forecast */}
          <Card
            w="50%"
            onClick={() => onForecastOpen()}
            cursor="pointer"
            boxShadow="md" // Add box shadow
            transition="box-shadow 0.2s ease-in-out" // Add transition effect
            _hover={{
              boxShadow: "lg", // Add larger box shadow on hover
            }}
          >
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px" maxWidth="80%">
                  <Heading fontSize="30px">Forecast</Heading>
                  <Text>
                    Get energy consumption predictions and price forecasts with
                    our advanced analytics tools. Make informed decisions and
                    plan accordingly
                  </Text>
                  <Text>Some graphs here for week</Text>
                  <Text>Some graphs here for month</Text>
                  <Text>Some graphs here for year</Text>
                </Flex>

                <Flex>
                  <Box pos="absolute" top="47px" right="30px">
                    <PlantSvg />
                  </Box>
                </Flex>
              </Flex>
            </CardBody>
          </Card>

          {/* Energy Efficiency */}
          <Card
            w="50%"
            onClick={() => onEnergyEfficiencyOpen()}
            cursor="pointer"
            boxShadow="md" // Add box shadow
            transition="box-shadow 0.2s ease-in-out" // Add transition effect
            _hover={{
              boxShadow: "lg", // Add larger box shadow on hover
            }}
          >
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px" w="80%">
                  <Heading fontSize="30px">
                    Energy Efficiency Statistics
                  </Heading>
                  <Text>
                    Measure and optimize your device energy efficiency. Save
                    energy, reduce costs and environmental impact.
                  </Text>
                </Flex>

                <Flex>
                  <Box pos="absolute" top="47px" right="30px">
                    <PlantSvg />
                  </Box>
                </Flex>
              </Flex>
            </CardBody>
          </Card>
        </Flex>

        <Flex flexDirection="row" gap="20px" justifyContent="center">
          {/* Report */}
          <Card
            w="60%"
            onClick={() => onReportOpen()}
            cursor="pointer"
            boxShadow="md" // Add box shadow
            transition="box-shadow 0.2s ease-in-out" // Add transition effect
            _hover={{
              boxShadow: "lg", // Add larger box shadow on hover
            }}
          >
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px" width="80%">
                  <Heading fontSize="30px">Download Report</Heading>
                  <Text>
                    Download detailed reports on your energy usage with our
                    dashboard. Take control of your consumption patterns and
                    optimize your usage.
                  </Text>
                </Flex>

                <Flex>
                  <Box paddingRight="30px">
                    <ReportSvg
                      width="60px"
                      height="60px"
                      className="moveAnimation"
                    />
                  </Box>
                </Flex>
              </Flex>
            </CardBody>
          </Card>
        </Flex>
      </Flex>
    </>

    // <Tabs isFitted variant='enclosed'>
    //     <TabList mb='1em'>
    //         <Tab>Carbon Footprint</Tab>
    //         <Tab>Forecast Energy Usage and Cost</Tab>
    //         <Tab>Energy Efficiency</Tab>
    //         <Tab>Report</Tab>
    //     </TabList>
    //     <TabPanels>
    //         <TabPanel>
    //             <CarbonChart />
    //         </TabPanel>
    //         <TabPanel>
    //             <ForecastChart />
    //         </TabPanel>
    //         <TabPanel>
    //             <EfficiencyBar />
    //         </TabPanel>
    //         <TabPanel>
    //             <Report />
    //         </TabPanel>
    //     </TabPanels>
    // </Tabs>
  );
}

export default Dashboard;
