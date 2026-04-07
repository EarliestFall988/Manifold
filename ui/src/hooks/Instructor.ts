// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-07 01:24:36 UTC

import { useQuery, useMutation } from "@tanstack/react-query";
import axios from "axios";
import type { ODataResponse } from "@/types/odata";
import type { InstructorType } from "@/types/InstructorType";

const getInstructor = (query?: string) =>
  axios
    .get<ODataResponse<InstructorType>>(`${import.meta.env.VITE_API_URL}/odata/Instructor${query ? `?${query}` : ""}`)
    .then((res) => res.data);

export const useInstructor = (query?: string) =>
  useQuery({
    queryKey: ["Instructor", query],
    queryFn: () => getInstructor(query),
  });

export const useCreateInstructor = () =>
  useMutation({
    mutationFn: (item: InstructorType) =>
      axios
        .post<InstructorType>(`${import.meta.env.VITE_API_URL}/odata/Instructor`, item)
        .then((res) => res.data),
  });

export const useUpdateInstructor = () =>
  useMutation({
    mutationFn: ({ key, delta }: { key: number; delta: Partial<InstructorType> }) =>
      axios
        .patch<InstructorType>(`${import.meta.env.VITE_API_URL}/odata/Instructor(${key})`, delta)
        .then((res) => res.data),
  });

export const useDeleteInstructor = () =>
  useMutation({
    mutationFn: (key: number) =>
      axios.delete(`${import.meta.env.VITE_API_URL}/odata/Instructor(${key})`),
  });

