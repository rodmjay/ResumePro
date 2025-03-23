export interface ResumeSettings {
  showEmail: boolean;
  showPhone: boolean;
  showLinkedIn: boolean;
  showGitHub: boolean;
  showLocation: boolean;
}

export interface ResumeDto {
  id: number;
  personId: number;
  organizationId: number;
  settings: ResumeSettings;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  linkedIn: string;
  gitHub: string;
  city: string;
  state: string;
  country: string;
  jobCount: number;
  skillCount: number;
  jobTitle: string;
  description: string;
}

export interface ResumeOptions {
  settings: ResumeSettings;
  skillIds: number[];
  companyIds: number[];
  description: string;
  jobTitle: string;
}

export interface ResumeSkill {
  id: number;
  title: string;
  categories: string[];
  rating: number;
}

export interface CategorySkillRating {
  category: string;
  skills: { title: string; rating: number }[];
}

export interface ResumeDetails extends ResumeDto {
  companies: any[]; // Will be typed as CompanyDetails[] after creating company models
  skills: ResumeSkill[];
  references: any[]; // Will be typed as ReferenceDto[] after creating reference models
  education: any[]; // Will be typed as SchoolDetails[] after creating education models
  languages: any[]; // Will be typed as PersonaLanguageDto[] after creating language models
  certifications: any[]; // Will be typed as CertificationDto[] after creating certification models
  renderings: any[]; // Will be typed as RenderingDto[] after creating rendering models
  skillDictionary: CategorySkillRating[];
  languageString?: string;
}
