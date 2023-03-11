import React, { useState, useEffect } from "react";
import axios from "axios";
import { BellIcon } from '@chakra-ui/icons'
import NotificationPopup from "./NotificationPopup";
import TestNotification from "./TestNotification";
import TestNotificationModal from "./TestNotificationModal";
import {
    Alert,
    AlertIcon,
    AlertTitle,
    AlertDescription,
    Popover,
    PopoverTrigger,
    PopoverContent,
    PopoverHeader,
    PopoverBody,
    PopoverFooter,
    PopoverArrow,
    PopoverCloseButton,
    PopoverAnchor,
    Flex,
    Box, 
    useDisclosure,
    Button,
    useToast,
} from '@chakra-ui/react'

import NotificationService from "../../requests/services/NotificationService";

export default function Notification() {

    const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";
    const {isOpen, onClose, onToggle} = useDisclosure();
    const [notifications, setNotifications] = useState([]);
    const [errors, setErrors] = useState(null);

    function getNotifications() {
        NotificationService.getNotificationsByAccountId(SESSION_ACCOUNT_GUID)
            .then(response => {
                console.log(response.data.data)
                setNotifications(response.data.data);
            }).catch(e => {
                console.log(e);
                setErrors({ 
                    statusCode: e.response.data.response.statusCode,
                    errorMessage: e.response.data.response.serverMessage
                });
            }) 
    }
    
    // On Component Load...
    useEffect(() => {
        getNotifications();
    }, [])

    const toast = useToast();
    const handleNotificationAPIError = () => {
        toast({
            title: "Error " + errors.statusCode,
            description: errors.errorMessage,
            status: "error",
            duration: 15000,
            isClosable: true
        });
    };

    return (
        <>
            { 
                // If there exists at least one error, return an alert for each of them
                errors &&
                handleNotificationAPIError()
            }

            {/* Notification bell modal */}
            <Popover>
                <PopoverTrigger>                  
                    <Button w={12} h={12} mr={4} onClick={isOpen ? onClose : onToggle}>
                        <BellIcon w={8} h={8}/>    
                    </Button> 
                </PopoverTrigger>
            
            <PopoverContent mt={5}>
                
                <PopoverHeader ml={2} fontWeight="bold" fontSize={25}>Welcome back xxxx</PopoverHeader>
                <PopoverBody>
                    <Flex
                        flexDirection="column"
                    >
                        <NotificationPopup
                            notifications={notifications}
                        />
                        <TestNotificationModal />
                    </Flex>
                </PopoverBody>
            </PopoverContent>
            </Popover>
        </>
        )
        
}