import {
	Heading,
	Box,
	Menu,
	MenuItem,
	MenuButton,
	Divider,
	MenuList,
	Button,
	Input,
	useDisclosure,
} from "@chakra-ui/react";
import { Link } from "react-router-dom";
import React, { useEffect, useState } from "react";
import JsonToTable from "components/Automation/JsonToEnergyLimitTable";
import ModalButton from "components/Automation/ModalButton";
import axios from "axios";





function List_EnergyLimits() {
	const { isOpen, onOpen, onClose } = useDisclosure();
	const [allRules, setAllRules] = useState([])
	useEffect(() => {
		async function getAllRules() {
			const response = await axios(
				`https://localhost:7140/api/Rules/GetAllRules`
			)
			const details = await response.data
			console.log(details);
			setAllRules(details)
			return
		}
		getAllRules().then(() => {
			console.log("all rules")
		})
				console.log(error);
			})
	}, [])
	return (
		<Box padding="6" w='100%'>
			<Heading alignContent="center">Energy Limit List</Heading>
			<br /><br />

			<JsonToTable ruleData={allRules} />
			<Box padding="3" display="flex">
				<Box width="50%" display="flex" justifyContent="flex-start">
					<Button ml={2} colorScheme="whatsapp">
						<Link to="/energyLimit/create/energylimit">Add EnergyLimit</Link>
					</Button>

				</Box>

			</Box>
		</Box>
	);
}

export default List_EnergyLimits