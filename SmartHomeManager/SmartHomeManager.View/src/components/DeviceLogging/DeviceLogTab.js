import React from 'react'
import {
    Tabs,
    TabList,
    TabPanels,
    Tab,
    TabPanel,
} from '@chakra-ui/react';

import DeviceLogSchedular from "components/DeviceLogging/DeviceLogOverview"
import SetLimit from "components/DeviceLogging/SetLimit"


function DeviceLogTab() {
    return (
        <Tabs isFitted variant='enclosed'>
            <TabList mb='1em'>
                <Tab>Device Log Overview</Tab>
                <Tab>Set device threshold</Tab>
            </TabList>
            <TabPanels>      
                <TabPanel>
                    <DeviceLogSchedular />
                </TabPanel>
                <TabPanel>
                    <SetLimit />
                </TabPanel>               
            </TabPanels>
        </Tabs>
    )
}

export default DeviceLogTab