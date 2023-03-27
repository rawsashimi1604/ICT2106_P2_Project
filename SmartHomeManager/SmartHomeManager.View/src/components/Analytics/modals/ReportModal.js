import React, { useState, useEffect } from "react";
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
import { DownloadIcon } from "@chakra-ui/icons";
import { LazyMotion, domAnimation, AnimatePresence } from "framer-motion";

import ReportService from "requests/services/ReportService";
import ErrorWidget from "../widgets/ErrorWidget";
import LoadingWidget from "../widgets/LoadingWidget";


function ReportModal({ onClose, size, isOpen }) {
  const [devices, setDevices] = useState([]);
  const [selectedDevice, setSelectedDevice] = useState(null);
  const [selectedTimeframe, setSelectedTimeframe] = useState(null);

  const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";

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

  const handleChange = (event) => {
    setSelectedDevice(event.target.value);
  };

  const handleTimeChange = (event) => {
    setSelectedTimeframe(event.target.value);
  };

  function getDevicesByGUID() {
    ReportService.getDevicesByGUID(SESSION_ACCOUNT_GUID)
      .then((response) => {
        console.log(response.data);
        setDevices(response.data.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  useEffect(() => {
    getDevicesByGUID();
  }, []);

  const handleClick = () => {
    if (!selectedDevice || !selectedTimeframe) {
      onErrorOpen()
      return
    }
    
    onLoadingOpen()

    // handling the post request...
    ReportService.getDeviceReport(
      selectedDevice,
      parseInt(selectedTimeframe)
    ).then((response) => {
      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement("a");
      const current = new Date();
      const date = `${current.getDate()}${
        current.getMonth() + 1
      }${current.getFullYear()}`;
      link.href = url;
      link.setAttribute(
        "download",
        date + "-" + selectedDevice + "-DeviceReport.pdf"
      );
      document.body.appendChild(link);
      link.click();
      
    });

    onLoadingClose()
  };

  const handleHouseholdClick = () => {
    if (!selectedTimeframe) {
      onErrorOpen()
      return
    }

    onLoadingOpen()

    ReportService.getHouseholdReport(
      SESSION_ACCOUNT_GUID,
      parseInt(selectedTimeframe)
    ).then((response) => {
      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement("a");
      const current = new Date();
      const date = `${current.getDate()}${
        current.getMonth() + 1
      }${current.getFullYear()}`;
      link.href = url;
      link.setAttribute(
        "download",
        date + "-" + SESSION_ACCOUNT_GUID + "HouseholdReport.pdf"
      );
      document.body.appendChild(link);
      link.click();
    });

    onLoadingClose()
  };

  return (
    <LazyMotion features={domAnimation}>
      <AnimatePresence>
        <Modal onClose={onClose} size={size} isOpen={isOpen}>
          <ErrorWidget onClose={onErrorClose} size="xl" isOpen={isErrorOpen} />

          <LoadingWidget
            onClose={onLoadingClose}
            size="xl"
            isOpen={isLoadingOpen}
          />

          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Download Report</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <Text>
                Download detailed reports on your energy usage with our
                dashboard. Take control of your consumption patterns and
                optimize your usage.
              </Text>
              <Box>
                <Box marginBottom="20px" marginTop="20px">
                  <Flex align="center">
                    <Select
                      placeholder="Select Timeframe"
                      value={selectedTimeframe}
                      onChange={handleTimeChange}
                    >
                      <option value="1">Last 1 Month</option>
                      <option value="3">Last 3 Months</option>
                      <option value="6">Last 6 Months</option>
                    </Select>
                  </Flex>
                </Box>

                <Box>
                  <Flex align="center" gap="10px" justify="space-between">
                    <Text minW="120px">Device Report:</Text>
                    <Select
                      placeholder="Select Device"
                      value={selectedDevice}
                      onChange={handleChange}
                      maxWidth="250px"
                    >
                      {devices?.map((device, i) => {
                        return (
                          <option key={i} value={device.deviceID}>
                            {device.deviceName} ({device.deviceID})
                          </option>
                        );
                      })}
                    </Select>
                    <Button onClick={handleClick} padding="10px">
                      <DownloadIcon marginRight="5px" />
                      Download
                    </Button>
                  </Flex>
                </Box>

                <Box marginTop="10px">
                  <Flex align="center" gap="10px" justify="space-between">
                    <Text minW="120px">Household Report:</Text>
                    <Button onClick={handleHouseholdClick} padding="10px">
                      <DownloadIcon marginRight="5px" />
                      Download
                    </Button>
                  </Flex>
                </Box>
              </Box>
            </ModalBody>
            <ModalFooter>
              <Button onClick={onClose}>Close</Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </AnimatePresence>
    </LazyMotion>
  );
}

export default ReportModal;
