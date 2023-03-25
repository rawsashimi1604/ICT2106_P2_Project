import HttpService from "requests/HttpService";

const GET_ALL_DEVICE_ENERGY_LIMIT_API_ROUTE = `https://localhost:7140/api/deviceEnergyLimit/all`;
const GET_ALL_DEVICE_ENERGY_LIMIT_API_ROUTE_MOCK = `https://jsonblob.com/api/1089178376589295616`;
const GET_DEVICE_ENERGY_LIMIT_BY_ID_API_ROUTE = `https://localhost:7140/api/deviceEnergyLimit/`;
const UPDATE_DEVICE_ENERGY_LIMIT_API_ROUTE =
    "https://localhost:7140/api/deviceEnergyLimit";

function getAllDeviceEnergyLimit() {
    //   return HttpService.get(GET_ALL_DEVICE_ENERGY_LIMIT_API_ROUTE);
    return HttpService.get(GET_ALL_DEVICE_ENERGY_LIMIT_API_ROUTE_MOCK);
}

function getDeviceEnergyLimitById(accountId) {
    return HttpService.get(GET_DEVICE_ENERGY_LIMIT_BY_ID_API_ROUTE + accountId);
}

function updateDeviceEnergyLimit(body) {
    return HttpService.post(UPDATE_DEVICE_ENERGY_LIMIT_API_ROUTE, body);
}

export default {
    getAllDeviceEnergyLimit,
    getDeviceEnergyLimitById,
    updateDeviceEnergyLimit,
};