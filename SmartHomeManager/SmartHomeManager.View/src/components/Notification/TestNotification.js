import React, { useState, useEffect } from "react";
import {
    FormControl,
    FormLabel,
    Input,
    Button,
    useToast,
    Toast,
  } from "@chakra-ui/react";
  
import NotificationService from "requests/services/NotificationService";

export default function TestNotification(){
  
  const SESSION_ACCOUNT_GUID = "11111111-1111-1111-1111-111111111111";
  const [errors, setErrors] = useState(null);

  // Form control
  const [input, setInput] = useState('')
  const handleInputChange = (e) => setInput(e.target.value)
  
  const toast = useToast();
  const handleClick = () => {
      // handling the post request...
      NotificationService.sendNotification(
        {
          "notificationObject": {
            "message": input,
            "accountId": SESSION_ACCOUNT_GUID
          }
        }
      ).then(response => {
        console.log(response)
        toast({
          title: response.data.responseObject.statusCode,
          description: `Notification sent, ${response.data.responseObject.serverMessage}`,
          status: "success",
          duration: 3000,
          isClosable: false
        });
      }).catch(e => {
        console.group(e)
        setErrors({ 
          statusCode: e.response.data.responseObject.statusCode,
          errorMessage: e.response.data.responseObject.serverMessage
        });
      })
  };

  useEffect(() => {
    if (errors) {
      toast({
        title: errors.statusCode,
        description: errors.errorMessage,
        status: "error",
        duration: 3000,
        isClosable: false
      });
    }
  }, [errors])

  return(
      <>
        <form>
          <FormControl>
            <FormLabel htmlFor="message">Message</FormLabel>
            <Input 
              type="text" 
              id="message" 
              placeholder="Notification Message"
              value={input}
              onChange={handleInputChange}
            />
          </FormControl>
          {/* <Button mt={4} type="submit">  */}
          <Button mt={4} onClick={handleClick}> 
            Submit
          </Button>
        </form>
      </>
    )
}