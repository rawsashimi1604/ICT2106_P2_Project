import React from "react";
import {
  Flex,
  Box,
  Select,
  Text,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
} from "@chakra-ui/react";
import { CgSpinnerAlt } from "react-icons/cg";
import { BiErrorCircle } from "react-icons/bi";
import "../Analytics.css";

function LoadingWidget({ onClose, size, isOpen }) {
  return (
    <Modal onClose={onClose} size={size} isOpen={isOpen}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader bg="yellow.200">Data loading</ModalHeader>
        <ModalBody bg="yellow.200">
          <Flex direction="row" align="center">
            <Box
              className="errorIcon"
              marginRight="0px"
              width="40px"
              height="40px"
            >
              <CgSpinnerAlt className="loaderIcon" />
            </Box>
            <span>
            Hang on, we are loading your data.
            </span>
          </Flex>
        </ModalBody>
      </ModalContent>
    </Modal>
  )  
}

export default LoadingWidget;
