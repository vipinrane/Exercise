/*
 * Copyright (C) 2009-2013 SonarSource SA
 * All rights reserved
 * mailto:contact AT sonarsource DOT com
 */
package org.sonar.samples.javascript;


import org.sonar.plugins.javascript.api.CustomJavaScriptRulesDefinition;

/**
 * Extension point to define a custom JavaScript rule repository.
 */
public class JavascriptCustomRulesDefinition extends CustomJavaScriptRulesDefinition {

  /**
   * Provide the repository name
   */
  @Override
  public String repositoryName() {
    return "Custom Repository";
  }

  /**
   * Provide the repository key
   */
  @Override
  public String repositoryKey() {
    return "javascript-custom-rules";
  }

  /**
   * Provide the list of checks class that implements rules
   * to be part of the rule repository
   */
  @Override
  public Class[] checkClasses() {
    return new Class[] {ForbiddenFunctionUseCheck.class} ;
  }
}
