import { type WeatherForecast } from "@/types/api.generated";
import type { ODataResponse } from "@/types/odata";
import axios from "axios";

export const getWeather = () =>
  axios.get<ODataResponse<WeatherForecast>>(`${import.meta.env.VITE_API_URL}/odata/WeatherForecast?$top=7&$orderby=Date desc`).then((res) => res.data);
