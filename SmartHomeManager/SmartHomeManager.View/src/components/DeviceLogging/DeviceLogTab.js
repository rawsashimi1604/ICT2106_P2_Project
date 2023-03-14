import React from 'react'
import {
    Tabs,
    TabList,
    TabPanels,
    Tab,
    TabPanel,
} from '@chakra-ui/react';

import DeviceLogging from "components/DeviceLogging/DeviceLogging";
import DeviceLogSchedular from "components/DeviceLogging/DeviceLogSchedular"


function DeviceLogTab() {
    return (
        <Tabs isFitted variant='enclosed'>
            <TabList mb='1em'>
                <Tab>Weekly Device Logs</Tab>
                <Tab>Set device threshold</Tab>
                <Tab>Device state</Tab>
            </TabList>
            <TabPanels>
                <TabPanel>
                    <DeviceLogging />
                </TabPanel>
                <TabPanel>
                    <DeviceLogging />
                </TabPanel>
                <TabPanel>
                    <DeviceLogSchedular />
                </TabPanel>
            </TabPanels>
        </Tabs>
    )
}

export default DeviceLogTab