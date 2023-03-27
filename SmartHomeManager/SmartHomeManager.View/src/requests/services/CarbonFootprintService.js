import HttpService from "requests/HttpService";

const GET_CARBON_FOOTPRINT_DATA_ROUTE = `https://localhost:7140/api/analysis/householdReport/carbonFootprint/`


function getCarbonFootprintData(accountId, year, month){
    return HttpService.get(`${GET_CARBON_FOOTPRINT_DATA_ROUTE}${accountId}/${year}-${month}`)
}



export default{
    getCarbonFootprintData,

}