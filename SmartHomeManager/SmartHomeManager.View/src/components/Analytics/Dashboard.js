import React from "react";
import { Flex, Card, CardBody, Text, Heading, Box } from "@chakra-ui/react";
import CarbonChart from "./CarbonChart";
import ForecastChart from "./ForecastChart";
import Report from "./Report";
import { EfficiencyBar } from "./EfficiencyBar";
import { ReactComponent as WeatherSvg } from "./assets/Weather.svg";
import { ReactComponent as PlantSvg } from "./assets/Plant.svg";
import { ReactComponent as ReportSvg } from "./assets/Report.svg";

function Dashboard() {
  return (
    <>
      {/* Dashboard */}
      <Flex flexDirection="column" gap="20px">
        <Flex flexDirection="row" gap="20px">
          {/* Welcome */}
          <Card w="50%">
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px">
                  <Heading>Welcome John Tan,</Heading>
                  <Text>Monday, 27 March 2023</Text>
                  <Text>Get valuable insights on your smart devices with our analytics dashboard. Monitor usage patterns and identify energy-saving opportunities. Take control of your energy consumption and make smarter choices for your home.</Text>
                </Flex>

                <Flex>
                  <WeatherSvg/>
                </Flex>
              </Flex>
            </CardBody>
          </Card>

          {/* Carbon footprint */}
          <Card w="50%">
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px" maxWidth="80%">
                  <Heading fontSize="30px">Carbon Footprint</Heading>
                  <Text>Carbon Footprint: Compare the energy usage of your household to national household statistics, measured in Watts. See the environmental impact of your Smart Devices.</Text>
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
          <Card w="50%">
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px" maxWidth="80%">
                  <Heading fontSize="30px">Energy Usage Forecast</Heading>
                  <Text>Make informed decisions with our energy usage forecast feature. Our advanced analytics tools allows you to predict your consumption. In addition, our platform forecasts energy prices, giving you the ability to plan and budget accordingly.</Text>
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
          <Card w="50%">
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px">
                  <Heading fontSize="30px">Energy Efficiency Statistics</Heading>
                  <Text>Measure and optimize your device energy efficiency. Save energy, reduce costs and environmental impact.</Text>
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

          {/* Report */}
          <Card w="60%">
            <CardBody>
              <Flex flexDirection="row" align="center" justify="space-between">
                <Flex flexDirection="column" gap="10px" width="80%">
                  <Heading fontSize="30px">Download Report</Heading>
                  <Text>Download detailed reports on your energy usage with our dashboard. Take control of your consumption patterns and optimize your usage.</Text>
                </Flex>

                <Flex>
                  <Box paddingRight="30px">
                    <ReportSvg width="60px" height="60px" className="moveAnimation"/>
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
