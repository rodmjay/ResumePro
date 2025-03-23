export interface PersonaDto {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  linkedIn: string;
  gitHub: string;
  city: string;
  state: string;
  country: string;
  resumeCount: number;
  skillCount: number;
}

export interface Skill {
  id: number;
  title: string;
}

export interface School {
  id: number;
  name: string;
  location: string;
  startDate: string;
  endDate?: string;
  degrees: Degree[];
}

export interface Degree {
  id: number;
  name: string;
  order: number;
}

export interface Language {
  code3: string;
  name: string;
  code2: string;
  nativeName: string;
}

export interface PersonaLanguage {
  code3: string;
  name: string;
  proficiency: number;
  proficiencyName: string;
}

export interface PersonaDetails extends PersonaDto {
  skills: Skill[];
  languages: PersonaLanguage[];
  schools: School[];
}

export interface PersonOptions {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  linkedIn: string;
  gitHub: string;
  city: string;
  stateId: number;
  country: string;
  languageOptions?: any[];
}

export interface PersonaFilters {
  searchTerm?: string;
}
