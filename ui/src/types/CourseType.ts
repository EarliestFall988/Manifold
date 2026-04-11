// AUTO GENERATED with ❤️ by Api.TypeGen
// Last Generated: 2026-04-11 21:07:16 UTC

export interface CourseType {
  Id: number;
  Name: string;
  Description: string;
  Weeks: number;
  Credits: number;
  StudentId: number | null;
  Instructor: InstructorType | null;
  Inserted: string;
  InsertedBy: string;
  Updated: string | null;
  UpdatedBy: string | null;
  Archived: boolean;
}
