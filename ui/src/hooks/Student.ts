// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-11 21:07:16 UTC

import { useQuery, useMutation } from "@tanstack/react-query";
import axios from "axios";
import type { ODataResponse } from "@/types/odata";
import type { StudentType } from "@/types/StudentType";

const getStudent = (query?: string) =>
  axios
    .get<ODataResponse<StudentType>>(`${import.meta.env.VITE_API_URL}/odata/Student${query ? `?${query}` : ""}`)
    .then((res) => res.data);

export const useStudent = (query?: string) =>
  useQuery({
    queryKey: ["Student", query],
    queryFn: () => getStudent(query),
  });

export const useCreateStudent = () =>
  useMutation({
    mutationFn: (item: StudentType) =>
      axios
        .post<StudentType>(`${import.meta.env.VITE_API_URL}/odata/Student`, item)
        .then((res) => res.data),
  });

export const useUpdateStudent = () =>
  useMutation({
    mutationFn: ({ key, delta }: { key: number; delta: Partial<StudentType> }) =>
      axios
        .patch<StudentType>(`${import.meta.env.VITE_API_URL}/odata/Student(${key})`, delta)
        .then((res) => res.data),
  });

export const useDeleteStudent = () =>
  useMutation({
    mutationFn: (key: number) =>
      axios.delete(`${import.meta.env.VITE_API_URL}/odata/Student(${key})`),
  });

