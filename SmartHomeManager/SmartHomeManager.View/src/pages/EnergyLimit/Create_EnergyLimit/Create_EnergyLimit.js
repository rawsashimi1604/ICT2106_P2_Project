import React, { useEffect, useState } from "react";
import {
	Box,
	Button,
	Heading,
	Flex,
	FormControl,
	FormLabel,
	Input,
	Select,
} from "@chakra-ui/react";
import { useLocation } from "react-router-dom";
import { useToast } from "@chakra-ui/react";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";

function Create_EnergyLimit() {
	return (
		<div>Create_EnergyLimit</div>
	)
}

export default Create_EnergyLimit