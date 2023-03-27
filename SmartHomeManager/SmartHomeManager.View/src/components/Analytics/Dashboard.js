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

import ErrorWidget from "./widgets/ErrorWidget";
import LoadingWidget from "./widgets/LoadingWidget";
import CarbonFootprintService from "requests/services/CarbonFootprintService";
import ForecastService from "requests/services/ForecastService";
import EnergyEfficiencyService from "requests/services/EnergyEfficiencyService";

function Dashboard() {

  const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";
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

  

  const [carbonData, setCarbonData] = useState(null)
  const [carbonNationalData, setCarbonNationalData] = useState(null)

  const [forecastData, setForecastData] = useState(null)
  const [forecastCostData, setForecastCostData] = useState(null)

  const [fanData, setFanData] = useState(null)
  const [lightData, setLightData] = useState(null)
  const [airconData, setAirconData] = useState(null)



  function loadDashboardData() {


    /*
      1. Create your states on top...
      2. Use the various services to Load the data into the states......
      3. Before you load the data.. you should open the loading modal...
      4. After has loaded finish close the loading modal...
      5. Use the data in the states to populate your dashboard.
    */
      onLoadingOpen();
      const today = new Date();
      const month = today.getMonth() + 1;
      const year = today.getFullYear();
      CarbonFootprintService.getCarbonFootprintData(SESSION_ACCOUNT_GUID, year, month).then((response) => {
        setCarbonData(response.data.data[0].householdConsumption.toFixed(2))
        setCarbonNationalData(response.data.data[0].nationalHouseholdConsumption.toFixed(2))
        return ForecastService.getHouseholdForecast(SESSION_ACCOUNT_GUID, 1)
      }).then((response) => {
        let totalWatt = 0 
        let totalCost = 0
        for(let month of response.data.data){
            totalWatt += month.wattsValue
            totalCost += month.priceValue
        }
        let averageWatt = totalWatt / response.data.data.length
        let averageCost = totalCost / response.data.data.length
        setForecastData(averageWatt.toFixed(2))
        setForecastCostData(averageCost.toFixed(2))
        return EnergyEfficiencyService.getEnergyEfficiencyData(SESSION_ACCOUNT_GUID)
      }).then((response) => {
        setFanData(response.data.data[0].energyEfficiencyIndex.toFixed(2))
        setAirconData(response.data.data[2].energyEfficiencyIndex.toFixed(2))
        setLightData(response.data.data[1].energyEfficiencyIndex.toFixed(2))
      }).then(() => {
      onLoadingClose(); 
      })
  }

  useEffect(() => {
    loadDashboardData();
  }, [])

  return (
    <>
      <ErrorWidget onClose={onErrorClose} size="xl" isOpen={isErrorOpen} />

      <LoadingWidget
        onClose={onLoadingClose}
        size="xl"
        isOpen={isLoadingOpen}
      />

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
      <Flex flexDirection="column" gap="40px">
        <Flex flexDirection="row" gap="40px">
          {/* Welcome */}
          <Card w="70%" boxShadow="md">
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
            w="70%"
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
                  <Text>This months household usage: {carbonData && carbonData} Watts</Text>
                  <Text>This months national usage: {carbonNationalData && carbonNationalData} Watts</Text>
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
            w="70%"
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
                  <Text>Forecasted Average Yearly Energy Usage : {forecastData && forecastData} Watts</Text>
                  <Text>Forecasted Average Yearly Cost : ${forecastCostData && forecastCostData}</Text>  
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
            w="70%"
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
                  <Text>
                    Smart Fan Energy Efficiency : {fanData && fanData}
                  </Text>
                  <Text>
                    Smart Blub Energy Efficiency : {lightData && lightData}
                  </Text>
                  <Text>
                    Smart Aircon Energy Efficiency : {airconData && airconData}
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
