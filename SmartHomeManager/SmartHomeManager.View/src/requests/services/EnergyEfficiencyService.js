import HttpService from "requests/HttpService";

const GET_ENERGY_EFFICIENCY_DATA_ROUTE = `https://localhost:7140/api/analysis/householdReport/energyEfficiency`

function getEnergyEfficiencyData(accountId){

    return HttpService.get(`${GET_ENERGY_EFFICIENCY_DATA_ROUTE}/${accountId}`)
}

export default{
  getEnergyEfficiencyData,
}