import HttpService from "requests/HttpService";

const GET_FORECAST_DATA_ROUTE = `https://localhost:7140/api/analysis/householdReport/energyUsageForecast/`


function getHouseholdForecast(accountId, timespan){

    return HttpService.get(`${GET_FORECAST_DATA_ROUTE}${accountId}/${timespan}`)
}



export default{
    getHouseholdForecast,

}