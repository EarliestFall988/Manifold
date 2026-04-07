// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-07 01:56:46 UTC

import type { CourseType } from "./CourseType";

export interface StudentType {
  Id: number;
  Name: string;
  Age: number;
  Courses: CourseType[];
  Inserted: string;
  InsertedBy: string;
  Updated: string | null;
  UpdatedBy: string | null;
  Archived: boolean;
}
