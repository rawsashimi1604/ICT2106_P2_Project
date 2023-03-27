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
import { BiErrorCircle } from "react-icons/bi";
import "../Analytics.css";

function ErrorWidget({ onClose, size, isOpen }) {
  return (
    <Modal onClose={onClose} size={size} isOpen={isOpen}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader bg="red.200">Something went wrong</ModalHeader>
        <ModalBody bg="red.200">
          <Flex direction="row" justify="center" align="center">
            <Box
              className="errorIcon"
              marginRight="0px"
              width="40px"
              height="40px"
            >
              <BiErrorCircle/>
            </Box>
            <span>
              Something went wrong in the backend service. Did you select the correct inputs?
            </span>
          </Flex>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
}

export default ErrorWidget;
